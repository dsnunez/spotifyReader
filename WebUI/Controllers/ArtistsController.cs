using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DomainModel;
using SpotifyMetadata;

namespace spotifyAcid.Controllers
{
    public class ArtistsController : Controller
    {
        private ArtistRepository repo = new ArtistRepository();

        // GET: Artists
        public ActionResult Index()
        {
            return View(repo.GetAllDownloadedArtists());
        }

        public ActionResult Search(string q)
        {
            if (String.IsNullOrWhiteSpace(q))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(repo.SearchArtist(q));
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

        public ActionResult Update(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = repo.UpdateArtistInfo(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            return View("Details", artist);
        }

        public ActionResult Download(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = repo.DownloadArtistFullInfo(id);
            return View("Details", artist);
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
