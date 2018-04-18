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

        // sukurkit resident forma (ji bus naudojama sukurimui residento arba modifikacijai)
        public ActionResult ResidentForm()
        {
            #region ViewBag
            RoomRepository roomRepository = new RoomRepository();
            List<Room> rooms = roomRepository.GetAll();
            DormitoryRepository dormitoryRepository = new DormitoryRepository();
            List<Dormitory> dormitories = dormitoryRepository.GetAll();


            List<SelectListItem> Dormitory = new List<SelectListItem>();
            if (dormitories.Count() == 0)
            {
                Dormitory.Add(new SelectListItem { Text = "Nėra registruotų bendrabučių", Value = "1" });
            }
            else
            {
                Dormitory.Add(new SelectListItem { Text = "", Value = "1" });
                for (int i = 0; i < dormitories.Count(); i++)
                {
                    var temp = new SelectListItem { Text = dormitories[i].Name, Value = (i + 2).ToString() };
                    Dormitory.Add(temp);

                }
            }
            ViewBag.Dormitory = Dormitory;

            List<SelectListItem> Rooms = new List<SelectListItem>();
            if (rooms.Count() == 0)
            {
                Rooms.Add(new SelectListItem { Text = "Nėra registruotų kambarių", Value = "1" });
            }
            else
            {
                Rooms.Add(new SelectListItem { Text = "", Value = "1" });
                for (int i = 0; i < rooms.Count(); i++)
                {
                    var temp = new SelectListItem { Text = rooms[i].Number.ToString(), Value = (i + 2).ToString() };
                    Rooms.Add(temp);

                }
            }
            ViewBag.Rooms = Rooms;
            #endregion
            return View();
        }

        // sukurkit svecio registravimo forma is residento puses
        public ActionResult RegisterGuest()
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