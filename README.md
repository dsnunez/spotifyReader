# Spotify Metadata Reader

**Spotify Metadata Reader** es una aplicación web, desarrollada en .NET, que permite descargar, desde [Spotify](https://www.spotify.com), la información sobre los álbumes y canciones de tus artistas favoritos, mostrando ciertas estadísticas de esta información. Este proyecto fue desarrollado como parte de una evaluación técnica.

## Uso de la aplicación

### Buscar un artista
Se puede buscar un artista utilizando el cuadro de búsqueda que aparece en la parte superior a lo largo de la aplicación.
La búsqueda abarcará los artistas cuya información ya se descargó ("aristas descargados") y los que están en Spotify, pero todavía no se descarga su información ("artistas online").

### Descargar información
Para poder ver las estadísticas y otros datos del artista, primero se debe descargar su información.
Esto se puede hacer desde los resultados de la búsqueda.

> **Nota:** Al momento de descargar los álbumes de un artista, se consideran:
> - Solo Los EP y LP del artista, dejando fuera sus *singles* y los *compilations* en los que solo hace alguna aparición.
> - Aquellos discos disponibles para el mercado chileno.


### Ver información del artista
Luego de descargar la información de un artista, es posible ver:
* Su lista de álbumes publicados, ordenados por año, del más reciente al más antiguo
* La portada de cada álbum (se asume que, de todas las imágenes provistas por Spotify para el álbum, la primera corresponde a la cubierta frontal del mismo)
* Estadísticas varias:
  * *Popularidad del álbum*: Es el valor entregado por la API de Spotify como popularidad del álbum, según la forma de calcularla que ellos tienen
  * *Popularidad promedio de las pistas*: La API de Spotify entrega, además, la popularidad de cada canción por separado. Aquí se promedia la popularidad de todas las canciones que componen el álbum
  * *Canción más larga*: De todas las canciones asociadas al álbum, se elige la que tiene una mayor duración, según la información entregada por Spotify, y se muestra su título y duración.
* Las lista de canciones que componen cada álbum (se debe hacer click en "ver canciones"). Cada canción se muestra con su: 
  * Número de pista
  * Título
  * Popularidad de la canción (la que se usa para calcular la *Popularidad promedio de las pistas* del álbum)
  * Duración

### Actualizar información de un artista
Cuando un artista ya ha sido descargado, pero se quiere volver a recuperar su información desde Spotify (por ejemplo, pensando en que quizás publicó un nuevo disco desde la última vez que accedimos), se puede "actualizar", haciendo uso de la opción disponible en la pantalla de "Artistas descargados".



## Implementación
**Spotify Metadata Reader** es una aplicación web implementada utilizando [ASP.NET MVC 5](http://www.asp.net/mvc/mvc5). La integración con la base de datos utiliza [Entity Framework](https://msdn.microsoft.com/en-us/data/ef.aspx), bajo el enfoque [Code-First](https://msdn.microsoft.com/en-us/data/jj193542.aspx).

Durante el desarrollo, también se utilizaron algunas herramientas externas, nombradas al final de esta sección.

### Módulos de la aplicación
El código consta de una solución (.sln) de **Visual Studio 2015** compuesta por tres projectos (o módulos):
* Domain Model (class library)
* SpotifyMetadata (class library)
* WebUI (ASP.NET Web Application)

#### DomainModel
Este es el módulo que se responsabiliza por el acceso a la base de datos. Aquí se enecuuentran los modelos de la base de datos (las clases utilizadas en el Code-First), la configuración de las migraciones y las migraciones mismas.

No hace uso de otros proyectos de la solución.

#### SpotifyMetadata
Este es el módulo que se responsabiliza por el acceso y edición a la información de los artistas, ya sea que esta se encuentre en Spotify y deba ser consultada a través de la API de este servicio, o que se encuentre descargada en la base de datos.

`ArtistRepository` es la principal clase de este proyecto y la encargada de entregar la información refente a los artistas, distinguiendo desde qué fuente debe obtenerla.

Hace uso del proyecto DomainModel.

##### Uso de la API de Spotify
La información de Spotify es accedida a través de la [Spotify Web API](https://developer.spotify.com/web-api/)
>Nota: La *API de Metadata de Spotify* [alcanzó su "end of life" el 20 de Enero del 2016](https://developer.spotify.com/technologies/metadata-api/) , por lo que forzosamente se debió utilizar la [Web API](https://developer.spotify.com/web-api/)

Para recibir e interpretar los JSON que entrega esta API, se utilizó el paquete NuGet **[Newtonsoft.Json](https://www.nuget.org/packages/newtonsoft.json/)** y, en la carpeta ResponseModels, se guardaron clases equivalentes a los objetos Artist, Album y Track (en sus dos versiones, *full* y *simplified*) del [Object Model](https://developer.spotify.com/web-api/object-model/) que utiliza la API de Spotify. También se guardaron representaciones de otros objetos que son referenciados por los ya mencionados.

Los *endpoints* de la API (dirección base: https://api.spotify.com) que se utilizan en esta aplicación son:

Endpoint | Uso en esta aplicación | Comentarios | Documentación
-------- | ---------------------- | ----------- | -------------
GET /v1/search | /search/?q={query}&type=artist | Se utiliza la query provista por el usuario para buscar artistas cuyo nombre coincida con la búsqueda. Nota: La API  de Spotify no arroja todos los resultados existentes cuando la query incluye un "dos puntos" (:). Por ejemplo, intentar https://api.spotify.com/v1/search/?q=d%3Aream&type=artist y comparar con los resultados que arroja la aplicación de escritorio si se busca el artista "d:ream" | [Search for an item](https://developer.spotify.com/web-api/search-item/)
GET /v1/artists/{id} | /artists/{id} | Una vez que se elige el artista a descargar, se recupera su información a través de su ID de Spotify. | [Get an Artist](https://developer.spotify.com/web-api/get-artist/)
GET /v1/artists/{id}/albums | /artists/{id}/albums?album_type=ep,album&market=CL | Dada una ID de artista, se descarga su lista de álbumes del tipo EP y ALBUM (`type=ep,album`) y que estén disponibles en el mercado chileno (`market=CL`). | [Get an Artist’s Albums](https://developer.spotify.com/web-api/get-artists-albums/)
GET /v1/albums/{id} | /albums/{id} | Dada la ID de un album (obtenida a través de la lista de álbumes de un artista), se descarga la información completa del álbum, incluyendo, por ejemplo, año de lanzamiento y popularidad del álbum. No incluye la popularidad individual de cada canción | [Get an Album](https://developer.spotify.com/web-api/get-album/)
GET /v1/albums/{id}/tracks | /albums/{id}/tracks | Dada la ID de un álbum, se descarga la lista con la información básica de cada una de sus pistas. | [Get an Album’s Tracks](https://developer.spotify.com/web-api/get-albums-tracks/)
GET /v1/tracks/{id} | /tracks/{id} | Dada la ID de una canción, se descarga su información completa, incluyendo, por ejemplo, su popularidad individual. | [Get a Track](https://developer.spotify.com/web-api/get-track/)

#### WebUI
Este módulo es la interfaz gráfica del sitio web. Utiliza los módulos DomainModel y SpotifyMetadata para funcionar y obtener la información de los artistas.

Es una aplicación web que no utiliza login.

La plantilla de *ASP.NET Web Application* incluye por defecto varios componentes, de ellos, solo se mantuvieron:
+ jQuery
+ Bootstrap
+ Parte del Site.css


### Otras herramientas y librerías utilizadas
Durante el desarrollo de este proyecto, se utilizaron (o incluyeron en el código) las siguientes herramientas externas:
* **[Kanbanery](https://dsnunez.kanbanery.com/projects/70697/board/?key=f171791a46c71075d7ef0f4f3c1a73580f9d998e)**: Para llevar cuenta de las tareas pendientes y realizadas. Cada *commit* de este repositorio fue asociado a alguna tarea del kanban mediante un identificador del tipo "#000000" en el mensaje, haciendo uso del *service hook* disponible entre GitHub y Kanbanery.
* **[MusicBox Theme](https://w3layouts.com/music-box-online-music-mobile-website-template)**: Template para Bootstrap adaptada para esta aplicación.
* **[CSS Percentege Circle](http://circle.firchow.net/)**: Hoja de estilos que facilitó la visualización de la popularidad de cada álbum.
* **[json2csharp](http://json2csharp.com/)**: Aplicación web capaz de leer objetos JSON y generar código de clases C# que los representa. Se usó para agilizar el mapeo de los objetos JSON que entrega la API de Spotify a clases de C#.
