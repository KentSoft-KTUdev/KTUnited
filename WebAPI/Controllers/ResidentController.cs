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
        private GuestRepository guestRepository = new GuestRepository();
        private VisitRepository visitRepository = new VisitRepository();

        // GET: Resident
        public ActionResult Index()
        {
            if(IsLoggedOn())
            {
                return RedirectToAction("ControlPanel");
            }
            return View();
        }

        public ActionResult RegisterGuest()
        {
            if (IsLoggedOn())
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterGuest([Bind(Include = "Name,Surname,PersonalCode")] Guest guest)
        {
            guestRepository.Create((guest));
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterVisitedGuest([Bind(Include = "GuestId")] Visit visit)
        {
            Resident temp = residentRepository.Read(Session["ResidentID"]);

            visit.IsOver = false;
            visit.ResidentId = temp.PersonalCode;
            visit.DormitoryId = temp.DormitoryId;
            //tikroji sukurimo data bus priskirta tada kai iseis zmogus t.y. kai patvirtins guard
            visit.VisitRegDateTime = DateTime.MaxValue;
            //same shit
            visit.VisitEndDateTime = DateTime.MaxValue;
            //cia reikes tureti viena default guarda, kuri priskirsim ir kai tikrasis tvirtina - pasikeicia i normalu
            visit.GuardId = guardRepository.Read(111).PersonalCode;
            visitRepository.Create(visit);
            List<SelectListItem> Guests = new List<SelectListItem>();
            foreach (Guest guest in guestRepository.GetAll())
            {
                Guests.Add(new SelectListItem { Text = guest.Name + " " + guest.Surname, Value = guest.PersonalCode.ToString() });

            }
            ViewBag.GuestId = Guests;
            return View(visit);
        }

        public ActionResult RegisterVisitedGuest()
        {
            #region ViewBag
            if(IsLoggedOn())
            {
                List<Guest> guest = guestRepository.GetAll();
                List<SelectListItem> Guests = new List<SelectListItem>();
                if (guest.Count == 0)
                {
                    Guests.Add(new SelectListItem { Text = "Pas jus niekas iki šiol nesilankė", Value = "-1" });
                }
                else
                {
                    for (int i = 0; i < guest.Count; i++)
                    {
                        Guests.Add(new SelectListItem { Text = guest[i].Name + " " + guest[i].Surname, Value = guest[i].PersonalCode.ToString() });
                    }
                }
                ViewBag.GuestId = Guests;
                #endregion
                return View();
            }
            return RedirectToAction("Index");
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