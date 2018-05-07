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
        private DormitoryRepository dormitoryRepository = new DormitoryRepository();
        private GuardRepository guardRepository = new GuardRepository();
        private RoomRepository roomRepository = new RoomRepository();
        private ResidentRepository residentRepository = new ResidentRepository();

        // GET: Resident
        public ActionResult Index()
        {
            if(IsLoggedOn())
            {
                return RedirectToAction("ControlPanel");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGuest([Bind(Include = "Name,Surname,PersonalCode")] Guest guest)
        {
            GuestRepository guestRepository = new GuestRepository();
            guestRepository.Create((guest));

            return RedirectToAction("Index");

        }

        public ActionResult RegisterGuest()
        {
            return View();
        }

        public ActionResult MyVisits()
        {
            #region ViewBag
            VisitRepository visitRepository = new VisitRepository();
            List<Visit> visit = visitRepository.GetAll();
            List<SelectListItem> Visits = new List<SelectListItem>();
            if (visit.Count == 0)
            {
                Visits.Add(new SelectListItem { Text = "No upcoming visits", Value = "1" });
            }
            else
            {
                for (int i = 0; i < visit.Count; i++)
                {
                    Visits.Add(new SelectListItem { Text = visit[i].VisitRegDateTime.ToString(), Value = (i + 1).ToString() });

                }
            }
            ViewBag.Visits = Visits;
            #endregion

            return View();
        }

        public ActionResult RegisterVisit()
        {
            return View();
        }

        public ActionResult RegisterVisitedGuest()
        {
            #region ViewBag
            GuestRepository guestRepository = new GuestRepository();
            List<Guest> guest = guestRepository.GetAll();
            List<SelectListItem> Guests = new List<SelectListItem>();
            if (guest.Count == 0)
            {
                Guests.Add(new SelectListItem { Text = "Pas jus niekas iki šiol nesilankė", Value = "1" });
            }
            else
            {
                for (int i = 0; i < guest.Count; i++)
                {
                    Guests.Add(new SelectListItem { Text = guest[i].Name, Value = (i + 1).ToString() });

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

        public ActionResult Login()
        {
            if (IsLoggedOn())
            {
                return RedirectToAction("ControlPanel");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Username,Password")]Resident resident)
        {
            if (residentRepository.Login(resident.Username, resident.Password).IsSuccessStatusCode)
            {
                resident = residentRepository.ReadByUsername(resident.Username);
                Session["Username"] = resident.Username;
                Session["ResidentID"] = resident.PersonalCode;
                return RedirectToAction("ControlPanel");
            }
            return RedirectToAction("Index", resident);
        }

        public ActionResult ControlPanel()
        {
            if (IsLoggedOn())
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        private bool IsLoggedOn()
        {
            if (Session["ResidentID"] != null && residentRepository.Read(Session["ResidentID"]).Username.Equals(Session["Username"]))
            {
                return true;
            }
            return false;
        }
    }
}