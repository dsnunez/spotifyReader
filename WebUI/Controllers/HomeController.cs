using System.Web.Mvc;

namespace spotifyAcid.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index","Artists",null);
        }
    }
}