using DataContract.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataContract.Objects;

namespace WebAPI.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

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


        public ActionResult RegisterDormitory() {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDormitory([Bind(Include = "Name,Adress")] Dormitory dormitory)
        {
            DormitoryRepository dormitoryRepository = new DormitoryRepository();
            dormitoryRepository.Create((dormitory));
        }

        public ActionResult RegisterGuard() {
            #region ViewBag
            
            return RedirectToAction("Index");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGuard([Bind(Include = "PersonalCode,Name,Surname,Dormitory")] Guard guard)
        {
            //DormitoryRepository dormitoryRepository = new DormitoryRepository();
            //List<Dormitory> dormitories = dormitoryRepository.GetAll();
            //for (int i = 0; i < dormitories.Count(); i++)
            //{
            //    if (dormitories[i].Name == guard.Dormitory.Name)
            //    {
            //        // add here, but the guard dormitory value is null because it only obtains string from web ?
            //    }

            //}
            GuardRepository guardRepository = new GuardRepository();
            guardRepository.Create((guard));
            return RedirectToAction("Index");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRoom([Bind(Include = "Dormitory,Number")] Room room)
        {
            //DormitoryRepository dormitoryRepository = new DormitoryRepository();
            //List<Dormitory> dormitories = dormitoryRepository.GetAll();
            //for (int i = 0; i < dormitories.Count(); i++)
            //{
            //    if (dormitories[i].Name == room.Dormitory.Name)
            //    {
            //        // add here, but the guard dormitory value is null because it only obtains string from web ?
            //    }

            //}
            RoomRepository roomRepository = new RoomRepository();
            roomRepository.Create((room));
            return RedirectToAction("Index");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateResident([Bind(Include = "Name,Surname,PersonalCode,Room,Dormitory")] Resident resident)
        {
            //DormitoryRepository dormitoryRepository = new DormitoryRepository();
            //List<Dormitory> dormitories = dormitoryRepository.GetAll();
            //for (int i = 0; i < dormitories.Count(); i++)
            //{
            //    if (dormitories[i].Name == guard.Dormitory.Name)
            //    {
            //        // add here, but the guard dormitory value is null because it only obtains string from web ?
                      // same with rooms
            //    }

            //}
            ResidentRepository residentRepository = new ResidentRepository();
            residentRepository.Create((resident));
            return RedirectToAction("Index");

        }


        public ActionResult RegisterRoom()
        {
            #region ViewBag
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
            #endregion
            RoomRepository rooms = new RoomRepository();
            List<Room> roomList = rooms.GetAll();
            if (roomList.Count() == 0)
            {
                Console.Write("Liudna");
            }


            return View();
        }

        public ActionResult RegisterGuard()
        {
            #region ViewBag
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
            #endregion
            GuardRepository guards = new GuardRepository();
            List<Guard> guardList = guards.GetAll();
            if (guardList.Count() == 0)
            {
                Console.Write("Liudna");
            }


            return View();

        }

    }
}