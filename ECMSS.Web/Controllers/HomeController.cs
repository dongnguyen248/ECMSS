using System.Web.Mvc;

namespace ECMSS.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AuthGate(string token)
        {
            Session["RequestToken"] = null;
            Session["RequestToken"] = token;
            return View("Index");
        }
    }
}