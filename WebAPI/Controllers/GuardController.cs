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

        public ActionResult Visits()
        {

            if (IsLoggedOn())
            {
                #region ViewBag
                List<Visit> visits = visitRepository.GetDormitoryVisits(guardRepository.Read(Session["GuardID"]).DormitoryId);
                List<Guard> guards = guardRepository.GetAll();
                ViewBag.Visits = visits;
                ViewBag.Guards = guards;
                #endregion
                return View();
            }
            return RedirectToAction("Index");
        }



        private bool IsLoggedOn()
        {
            if (Session["GuardID"] != null && guardRepository.Read(Session["GuardID"]).Username.Equals(Session["Username"]))
            {
                return true;
            }
            return false;
        }

        public ActionResult ControlPanel()
        {
            return View();
        }

        public ActionResult ConnectionNotSuccessful()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Username,Password")]Guard guard)
        {
            if (guardRepository.Login(guard.Username, DataContract.Configuration.Encryption(guard.Password)).IsSuccessStatusCode)
            {
                guard = guardRepository.ReadByUsername(guard.Username);
                Session["Username"] = guard.Username;
                Session["GuardID"] = guard.PersonalCode;
                return RedirectToAction("ControlPanel");
            }
            return RedirectToAction("ConnectionNotSuccessful", guard);
        }

        public ActionResult Approve()
        {
            object visitID = RouteData.Values["id"];
            Visit visit = visitRepository.Read(visitID);
            visit.VisitRegDateTime = DateTime.Now;
            visit.GuardId = (Int64)Session["GuardID"];
            visit.IsConfirmed = true;
            visit.IsOver = false;
            visitRepository.Update(visitID, visit);
            return RedirectToAction("Visits");
        }

        public ActionResult EndVisit()
        {
            object visitID = RouteData.Values["id"];
            Visit visit = visitRepository.Read(visitID);
            visit.VisitEndDateTime = DateTime.Now;
            visit.GuardId = (Int64)Session["GuardID"];
            visit.IsOver = true;
            visitRepository.Update(visitID, visit);
            return RedirectToAction("Visits");
        }
    }
}