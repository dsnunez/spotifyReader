// descargando información del artista

// obtener lista de álbumes (nombres + id)

// por cada álbum
// // descargando track

function downloadArtist(id) {
    var response = excecuteAction("Artist", "DownloadArtistInfo", id);
    var artistInfo = response.data;
}


function downloadAlbumList(id) {

}

function downloadTrack(id) {

}
