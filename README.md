# Spotify Metadata Reader

**Spotify Metadata Reader** es una aplicación web que permite descargar desde [Spotify](https://www.spotify.com) la información sobre los álbumes y canciones de tus artistas favoritos, mostrando ciertas estadísticas de esta información. Este proyecto fue desarrollado como parte de una evaluación técnica.

## Uso de la aplicación

### Buscar un artista
Se puede buscar utilizando el cuadro de búsqueda que aparece en la parte superior a lo largo de la aplicación.
La búsqueda abarcará los artistas cuya información ya se descargó ("aristas descargados") y los que están en Spotify, pero todavía no se descarga su información ("artistas online").

### Descargar información
[pendiente]

### Ver lista de álbumes
[pendiente]

> **Nota:** Al hablar de "álbum" se consideran solo los EP y LP del artista, dejando fuera sus *singles* y los *compilations* en los que solo hace alguna aparición

### Actualizar información de un artista
[pendiente]

## Implementación
**Spotify Metadata Reader** es una aplicación web implementada utilizando [ASP.NET MVC 5](http://www.asp.net/mvc/mvc5). La integración con la base de datos utiliza [Entity Framework](https://msdn.microsoft.com/en-us/data/ef.aspx), bajo el enfoque [Code-First](https://msdn.microsoft.com/en-us/data/jj193542.aspx)

La información de Spotify es accedida a través de la [Spotify Web API](https://developer.spotify.com/web-api/)
>Nota: La *API de Metadata de Spotify* [alcanzó su "end of life"](https://developer.spotify.com/technologies/metadata-api/) el 20 de Enero del 2016, por lo que forzosamente se debió utilizar la [Web API](https://developer.spotify.com/web-api/)

### Módulos de la aplicación
[pendiente]

#### DomainModel
[pendiente]

#### SpotifyMetadata
[pendiente]

#### WebUI
[pendiente]


### Uso de la API  de Spotify
[pendiente]

### Otras herramientas y librerías utilizadas
* **[Kanbanery](https://dsnunez.kanbanery.com/projects/70697/board/?key=f171791a46c71075d7ef0f4f3c1a73580f9d998e)**: Para llevar cuenta de las tareas pendientes y realizadas. Cada *commit* fue asociado a alguna tarea del kanban mediante un identificador del tipo "#000000" y el *service hook* disponible entre GitHub y Kanbanery.
* **[MusicBox Theme](https://w3layouts.com/music-box-online-music-mobile-website-template)**: Template para Bootstrap adaptada para esta aplicación.
* **[CSS Percentege](http://circle.firchow.net/) Circle**: Hoja de estilos que facilitó la visualización de la popularidad de cada álbum.
* **[json2csharp](http://json2csharp.com/)**: Para agilizar el mapeo de los objetos JSON que entrega la API de Spotify a clases de C#, lo que a su vez agiliza su deserialización.