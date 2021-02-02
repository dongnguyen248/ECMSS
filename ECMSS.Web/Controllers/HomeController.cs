using System.Web.Mvc;

namespace ECMSS.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AuthGate(string empId)
        {
            Session["RequestToken"] = empId;
            return RedirectToAction("Index");
        }
    }
}