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

        // GET: Artists/Details/5
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
            return View(artist);
        }
    }
}
