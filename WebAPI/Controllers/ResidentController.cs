using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataContract.Data;
using DataContract.Objects;

namespace WebAPI.Controllers
{
    public class ResidentController : Controller
    {
        // GET: Resident
        public ActionResult Index()
        {
            return View();
        }
        // sukurkit svecio registravimo forma is residento puses

        [HttpPost]
        public ActionResult RegisterGuestInfo()
        {
            string name = Request["firstname"].ToString();
            string surname = Request["lastname"].ToString();
            int personalID = Convert.ToInt32(Request["personalcode"].ToString());


            return View();
        }

        public ActionResult RegisterGuest()
        {
            return View();
        }

        public ActionResult RegisterVisit()
        {
            return View();
        }

        public ActionResult RegisterVisitedGuest()
        {
            #region ViewBag
            //GuestRepository guestRepository = new GuestRepository();
            //List<Guest> guest = guestRepository.GetAll();
            int laikinasCount = 0;
            List<SelectListItem> Guests = new List<SelectListItem>();
            if (laikinasCount == 0)
            {
                Guests.Add(new SelectListItem { Text = "Pas jus niekas iki šiol nesilankė", Value = "1" });
            }
            else
            {
                for (int i = 0; i < laikinasCount; i++)
                {
                    //var temp = new SelectListItem { Text = Guest[i].Name, Value = (i + 1).ToString() };
                    //Guests.Add(temp);

                }
            }
            ViewBag.Guests = Guests;
            #endregion
            return View();
        }

        // sukurkit kazka kas butu kaip to gyventojo vizitu sarasas
        public ActionResult Visits()
        {
            return View();
        }
    }
}