$(document).ready(function () {
    $(".download-spotify").click(function () {
        var id = $(this).data("id");
        downloadArtistInfo(id);
    });

    $(".upload-spotify").click(function () {
        var id = $(this).data("id");
        downloadArtistInfo(id);
    });
});

function downloadArtistInfo(id) {
    // descargando información del artista
    // obtener lista de álbumes (nombres + id)
    var artistInfo = excecuteAction("Artists", "DownloadArtistInfo", id);

    // por cada álbum
    // // descargando track
    var artistSavedId = artistInfo.id;
    var maxAlbum = artistInfo.albums.length;
    for (var i = 0; i < maxAlbum; i++) {
        var album = artistInfo.albums[i];
        var albumSpotifyId = album.Key;
        var albumName = album.Value;
        var trackList = excecuteActionMultipleParams("Artists", "DownloadAlbumInfo", { id: albumSpotifyId, artist: artistSavedId });

        var albumSavedId = trackList.id
        var maxTrack = trackList.tracks.length;
        for (var j = 0; j < maxTrack; j++) {
            var track = trackList.tracks[j];
            var trackSpotifyId = track.Key;
            var trackName = track.Value;
            var viewParams = {
                albumIndex: i,
                trackIndex: j,
                maxAlbum: maxAlbum,
                maxTrack: maxTrack,
                artistSavedId: artistSavedId
            };
            excecuteActionMultipleParams("Artists", "DownloadTrackInfo", { id: trackSpotifyId, album: albumSavedId }, updateView, viewParams)
        }
    }
}
function updateView(viewParams) {
    if (viewParams.albumIndex + 1 == viewParams.maxAlbum && viewParams.trackIndex + 1 == viewParams.maxTrack) {
        window.location.href = "/Artists/Details/" + viewParams.artistSavedId;
    }
}