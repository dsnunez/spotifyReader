$(document).ready(function () {
    $(".download-spotify").click(function () {
        var id = $(this).data("id");
        var name = $(this).data("name");
        downloadArtistInfo(id,name);
    });

    $(".update-spotify").click(function () {
        var id = $(this).data("id");
        var name = $(this).data("name");
        downloadArtistInfo(id,name);
    });
});

function downloadArtistInfo(id,name) {
    $("#download-artist-name").text(name);
    $("#downloading-artist-info").hide()
    show_modal("download-modal");

    // descargando información del artista
    // obtener lista de álbumes (nombres + id)
    var artistInfo = excecuteAction("Artists", "DownloadArtistInfo", id);

    // por cada álbum
    // // descargando track
    var artistSavedId = artistInfo.id;
    var maxAlbum = artistInfo.albums.length;
    var img = artistInfo.albums[0].Img;

    $("#download-info").html('<img id="downloading-img" src="'+img+'" height="100" width="100" />');
    $("#downloading-artist-info").show();

    for (var i = 0; i < maxAlbum; i++) {
        var album = artistInfo.albums[i];
        $("#downloading-img").attr("src", album.Img);
        $("#download-progress-track").width("0%");
        var albumSpotifyId = album.Key;
        var albumName = album.Value;
        $("#downloading-text-album").text("\"" + albumName + "\"");
        var trackList = excecuteActionMultipleParams("Artists", "DownloadAlbumInfo", { id: albumSpotifyId, artist: artistSavedId });

        var albumSavedId = trackList.id
        var maxTrack = trackList.tracks.length;
        for (var j = 0; j < maxTrack; j++) {
            var track = trackList.tracks[j];
            var trackSpotifyId = track.Key;
            var trackName = track.Value;
            var viewParams = {
                albumIndex: i,
                albumName: albumName,
                trackIndex: j,
                trackName: trackName,
                maxAlbum: maxAlbum,
                maxTrack: maxTrack,
                artistSavedId: artistSavedId
            };
            excecuteActionMultipleParams("Artists", "DownloadTrackInfo", { id: trackSpotifyId, album: albumSavedId }, updateView, viewParams)
        }
    }
}
function updateView(viewParams) {
    var prevAlbum = viewParams.albumIndex;
    var currAlbum = viewParams.albumIndex + 1;
    var maxAlbum = viewParams.maxAlbum;

    var currTrack = viewParams.trackIndex + 1;
    var trackName = viewParams.trackName;
    var maxTrack = viewParams.maxTrack;

    if (currAlbum == maxAlbum && currTrack == maxTrack) {
        $("#download-progress-album").width(100 + "%");
        $("#download-progress-track").width(100 + "%");
        window.location.href = "/Artists/Details/" + viewParams.artistSavedId;
    }
    else {
        var trackProgress = currTrack / maxTrack;
        var albumProgress = (prevAlbum + trackProgress) / maxAlbum;
        $("#download-progress-album").width(albumProgress * 100 + "%");
        $("#downloading-text-track").text("Canción \"" + trackName + "\"...");
        $("#download-progress-track").width(trackProgress * 100 + "%");
    }
}