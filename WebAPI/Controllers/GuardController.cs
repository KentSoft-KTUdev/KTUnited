using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataContract.Data;
using DataContract.Objects;

namespace WebAPI.Controllers
{
    public class GuardController : Controller
    {

        private DormitoryRepository dormitoryRepository = new DormitoryRepository();
        private GuardRepository guardRepository = new GuardRepository();
        private RoomRepository roomRepository = new RoomRepository();
        private ResidentRepository residentRepository = new ResidentRepository();
        private GuestRepository guestRepository = new GuestRepository();
        private VisitRepository visitRepository = new VisitRepository();

        // GET: Guard
        public ActionResult Index()
        {
            if (IsLoggedOn())
            {
                return RedirectToAction("ControlPanel");
            }
            return View();
        }

        public ActionResult Visits() {

            if (IsLoggedOn())
            {
                #region ViewBag
                List<Visit> visits = visitRepository.GetResidentVisits((long)Session["ResidentID"]);
                List<Guest> guests = guestRepository.GetResidentGuests((long)Session["ResidentID"]);
                List<Guard> guards = guardRepository.GetAll();
                ViewBag.Visits = visits;
                ViewBag.Guests = guests;
                ViewBag.Guards = guards;
                #endregion

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

        public ActionResult ControlPanel() {
            return View();
        }

        public ActionResult ConnectionNotSuccessful()
        {
            return View();
        }

        // temporary change of object from guard to resident
        // needs fixing after backend fix
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Username,Password")]Resident guard)
        {
            if (residentRepository.Login(guard.Username, guard.Password).IsSuccessStatusCode)
            {
                guard = residentRepository.ReadByUsername(guard.Username);
                Session["Username"] = guard.Username;
                Session["ResidentID"] = guard.PersonalCode;
                return RedirectToAction("ControlPanel");
            }
            return RedirectToAction("ConnectionNotSuccessful", guard);
        }



    }
}