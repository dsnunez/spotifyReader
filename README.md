# Spotify Metadata Reader

**Spotify Metadata Reader** es una aplicación web que permite descargar desde [Spotify](https://www.spotify.com) la información sobre los álbumes y canciones de tus artistas favoritos, mostrando ciertas estadísticas de esta información. Este proyecto fue desarrollado como parte de una evaluación técnica.

## Uso de la aplicación

### Buscar un artista
Se puede buscar utilizando el cuadro de búsqueda que aparece en la parte superior a lo largo de la aplicación.
La búsqueda abarcará los artistas cuya información ya se descargó ("aristas descargados") y los que están en Spotify, pero todavía no se descarga su información ("artistas online").

### Descargar información
Para poder ver las estadísticas del artista, primero se debe descargar su información.
Esto se puede hacer en los resultados de la búsqueda.

> **Nota:** Al momento de descargar los álbumes de un artista, se consideran solo:
> - Los EP y LP del artista, dejando fuera sus *singles* y los *compilations* en los que solo hace alguna aparición.
> - Aquellos discos disponibles para el mercado chileno.


### Ver lista de álbumes
Luego de descargar la información de un artista, es posible ver:
* Su lista de álbumes ordenados por año, del más reciente al más antiguo
* La portada de cada álbum (se asume que, de todas las imágenes provistas por Spotify para el álbum, la primera corresponde a la cubierta frontal del mismo)
* Las lista de canciones que componen cada álbum (se debe hacer click en "ver canciones")
* Estadísticas varias:
  * *Popularidad del álbum*: Es el valor entregado por la API de Spotify como popularidad del álbum, según la forma de calcularla que ellos tienen
  * *Popularidad promedio de las pistas*: La API de Spotify entrega, además, la popularidad de cada canción por separado. Aquí se promedia la popularidad de todas las canciones que componen el álbum
  * *Canción más larga*: De todas las canciones asociadas al álbum, se elige la que tiene una mayor duración según la información entregada por Spotify

### Actualizar información de un artista
Cuando un artista ya ha sido descargado, pero se quiere volver a recuperar su información desde Spotify (por ejemplo, pensando en que quizás publicó un nuevo disco desde la última vez que accedimos), se puede "actualizar", haciendo uso de la opción disponible en la pantalla de "Artistas descargados".



## Implementación

### Módulos de la aplicación
**Spotify Metadata Reader** es una aplicación web implementada utilizando [ASP.NET MVC 5](http://www.asp.net/mvc/mvc5). La integración con la base de datos utiliza [Entity Framework](https://msdn.microsoft.com/en-us/data/ef.aspx), bajo el enfoque [Code-First](https://msdn.microsoft.com/en-us/data/jj193542.aspx).
El código consta de una solución compuesta por tres projectos:
* Domain Model (class library)
* SpotifyMetadata (class library)
* WebUI (ASP.NET Web Application)

#### DomainModel
[pendiente]

#### SpotifyMetadata
[pendiente]
#### Uso de la API de Spotify
La información de Spotify es accedida a través de la [Spotify Web API](https://developer.spotify.com/web-api/)
>Nota: La *API de Metadata de Spotify* [alcanzó su "end of life"](https://developer.spotify.com/technologies/metadata-api/) el 20 de Enero del 2016, por lo que forzosamente se debió utilizar la [Web API](https://developer.spotify.com/web-api/)

#### WebUI
[pendiente]


### Otras herramientas y librerías utilizadas
* **[Kanbanery](https://dsnunez.kanbanery.com/projects/70697/board/?key=f171791a46c71075d7ef0f4f3c1a73580f9d998e)**: Para llevar cuenta de las tareas pendientes y realizadas. Cada *commit* fue asociado a alguna tarea del kanban mediante un identificador del tipo "#000000" y el *service hook* disponible entre GitHub y Kanbanery.
* **[MusicBox Theme](https://w3layouts.com/music-box-online-music-mobile-website-template)**: Template para Bootstrap adaptada para esta aplicación.
* **[CSS Percentege Circle](http://circle.firchow.net/)**: Hoja de estilos que facilitó la visualización de la popularidad de cada álbum.
* **[json2csharp](http://json2csharp.com/)**: Para agilizar el mapeo de los objetos JSON que entrega la API de Spotify a clases de C#, lo que a su vez agiliza su deserialización.
