using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAPI.Controllers
{
    public class ResidentController : Controller
    {
        // GET: Resident
        public ActionResult Index()
        {
            return View();
        }

        // sukurkit resident forma (ji bus naudojama sukurimui residento arba modifikacijai)
        public ActionResult ResidentForm()
        {
            return View();
        }

        // sukurkit svecio registravimo forma is residento puses
        public ActionResult RegisterGuest()
        {
            return View();
        }

        // sukurkit kazka kas butu kaip to gyventojo vizitu sarasas
        public ActionResult Visits()
        {
            return View();
        }
    }
}