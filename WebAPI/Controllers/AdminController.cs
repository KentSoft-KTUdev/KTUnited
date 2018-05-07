using DataContract.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataContract.Objects;
using System.Net.Http;

namespace WebAPI.Controllers
{
    public class AdminController : Controller
    {
        private AdministratorRepository administratorRepository = new AdministratorRepository();
        private DormitoryRepository dormitoryRepository = new DormitoryRepository();
        private GuardRepository guardRepository = new GuardRepository();
        // GET: Admin
        public ActionResult Index(Administrator administrator)
        {
            return View(administrator);
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


        public ActionResult RegisterDormitory()
        {
            if (Session["Username"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDormitory([Bind(Include = "Name,Adress")] Dormitory dormitory)
        {
            dormitoryRepository.Create((dormitory));
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGuard([Bind(Include = "PersonalCode,Name,Surname,DormitoryId,Username,Password")] Guard guard)
        {
            if(guardRepository.Create((guard)).IsSuccessStatusCode)
            {
                return View();
            }
            else
            {
                return View("RegisterGuard", guard);
            } 
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

        public ActionResult RegisterGuard(Guard guard)
        {
            if (Session["Username"] != null)
            {
                #region ViewBag
                List<Dormitory> dormitories = dormitoryRepository.GetAll();
                List<SelectListItem> Dormitory = new List<SelectListItem>();
                if (dormitories.Count == 0)
                {
                    Dormitory.Add(new SelectListItem { Text = "Nėra registruotų bendrabučių", Value = "-1" });
                }
                else
                {
                    //default value
                    Dormitory.Add(new SelectListItem { Text = "", Value = "-1" });
                    for (int i = 0; i < dormitories.Count; i++)
                    {
                        var temp = new SelectListItem { Text = dormitories[i].Name, Value = dormitories[i].ID.ToString() };
                        Dormitory.Add(temp);
                    }
                }
                ViewBag.Dormitory = Dormitory;
                ViewBag.DefaultPassword = GetRandomAlphaNumeric();
                #endregion
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Username,Password")]Administrator administrator)
        {
            if (administratorRepository.Login(administrator.Username, administrator.Password).IsSuccessStatusCode)
            {
                Session["Username"] = administrator.Username;
                return RedirectToAction("ControlPanel");
            }
            return RedirectToAction("Index", administrator);
        }

        public ActionResult ControlPanel()
        {
            if (Session["Username"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        private string GetRandomAlphaNumeric()
        {
            Random random = new Random();
            var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(chars.Select(c => chars[random.Next(chars.Length)]).Take(8).ToArray());
        }
    }
}