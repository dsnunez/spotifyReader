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
            Artist artist = repo.DownloadArtistInfo(id);
            return View("Details", artist);
        }
    }
}
