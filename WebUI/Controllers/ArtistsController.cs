using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DomainModel;
using SpotifyMetadata;

namespace spotifyAcid.Controllers
{
    public class ArtistsController : Controller
    {
        private ArtistRepository repo = new ArtistRepository();

        // GET: Artists
        public ActionResult Index(int page = 1, int perPage = 5)
        {
            var artists = repo.GetAllDownloadedArtists(page, perPage);
            return View(artists);
        }

        [HttpPost]
        public ActionResult Search(string q, int page = 1, int perPage = 5)
        {
            if (String.IsNullOrWhiteSpace(q))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(repo.SearchArtist(q, page, perPage));
        }

        public ActionResult SearchDownloadedPartial(string q = "", int page = 1, int perPage = 5)
        {
            ViewData.Add("search", !String.IsNullOrWhiteSpace(q));
            ViewData.Add("searchQuery", String.IsNullOrWhiteSpace(q) ? "" : q);
            ViewData.Add("searchType", "downloaded");
            ViewData.Add("parentDiv", "artists-downloaded-div");

            var artists = String.IsNullOrWhiteSpace(q) ? repo.GetAllDownloadedArtists(page, perPage) : repo.SearchDownloaded(q, page, perPage);
            return PartialView("_ArtistsDownloadedTable", artists);
        }

        public ActionResult SearchSpotifyPartial(string q, int page = 1, int perPage = 5)
        {
            if (String.IsNullOrWhiteSpace(q))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewData.Add("search", true);
            ViewData.Add("searchQuery", q);
            ViewData.Add("searchType", "spotify");
            ViewData.Add("parentDiv", "artists-spotify-div");

            var artists = repo.SearchSpotify(q, page, perPage);
            return PartialView("_ArtistsSpotifyTable", artists);
        }

        // GET: Artists/View/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = repo.GetArtistById(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            artist.Albums = artist.Albums.OrderByDescending(a => a.Year).ToList();
            return View(artist);
        }

        public ActionResult DownloadArtistInfo(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var artistSavedId = repo.DownloadArtistBasicInfo(id);
            var albumsAndIds = repo.DownloadArtistAlbumList(id);
            //Devolver JSON con {nombre, id} de cada album

            return new JsonResult()
            {
                Data = new
                {
                    id = artistSavedId,
                    albums = albumsAndIds,
                }
            };
        }
        public ActionResult DownloadAlbumInfo(string id, int artist)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var albumSavedId = repo.DownloadAlbumBasicInfo(id, artist);
            var tracksAndIds = repo.DownloadAlbumTrackList(id);
            //Devolver JSON con {nombre, id} de cada track
            return new JsonResult()
            {
                Data = new
                {
                    id = albumSavedId,
                    tracks = tracksAndIds
                }
            };
        }
        public ActionResult DownloadTrackInfo(string id, int album)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            repo.DownloadTrack(id, album);
            return new JsonResult()
            {
                Data = new
                {
                    message = "ok"
                }
            };
        }

        public ActionResult RestartDatabase()
        {
            repo.DeleteAll();
            return RedirectToAction("Index");
        }
    }
}
