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
        private RoomRepository roomRepository = new RoomRepository();
        private ResidentRepository residentRepository = new ResidentRepository();

        // GET: Admin
        public ActionResult Index(Administrator administrator)
        {
            return View(administrator);
        }

        public ActionResult RegisterResident()
        {
            #region ViewBag
            if (Session["Username"] != null)
            {
                List<Dormitory> dormitories = dormitoryRepository.GetAll();
                List<SelectListItem> Dormitory = new List<SelectListItem>();
                if (dormitories.Count == 0)
                {
                    Dormitory.Add(new SelectListItem { Text = "Nėra registruotų bendrabučių", Value = "-1" });
                }
                else
                {
                    Dormitory.Add(new SelectListItem { Text = "", Value = "-1" });
                    for (int i = 0; i < dormitories.Count(); i++)
                    {
                        var temp = new SelectListItem { Text = dormitories[i].Name, Value = dormitories[i].ID.ToString() };
                        Dormitory.Add(temp);

                    }
                }
                ViewBag.DormitoryId = Dormitory;
                List<Room> rooms = roomRepository.GetAll();
                List<SelectListItem> Rooms = new List<SelectListItem>();
                if (rooms.Count == 0)
                {
                    Rooms.Add(new SelectListItem { Text = "Nėra registruotų kambarių", Value = "-1" });
                }
                else
                {
                    Rooms.Add(new SelectListItem { Text = "", Value = "-1" });
                    for (int i = 0; i < rooms.Count; i++)
                    {
                        var temp = new SelectListItem { Text = rooms[i].Number.ToString(), Value = rooms[i].ID.ToString() };
                        Rooms.Add(temp);
                    }
                }
                ViewBag.RoomId = Rooms;
                ViewBag.DefaultPassword = GetRandomAlphaNumeric();
                #endregion
                return View();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterResident([Bind(Include = "Name,Surname,PersonalCode,RoomId,DormitoryId,Password,Username")] Resident resident)
        {
            ViewBag.RoomId = new SelectList(dormitoryRepository.GetAll(), "ID", "Name", resident.RoomId);
            ViewBag.DormitoryId = new SelectList(dormitoryRepository.GetAll(), "ID", "Name", resident.DormitoryId);
            ViewBag.DefaultPassword = resident.Password;
            residentRepository.Create((resident));
            return View();

        }


        public ActionResult CreateDormitory()
        {
            if (Session["Username"] != null)
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDormitory([Bind(Include = "Name,Adress")] Dormitory dormitory)
        {
            dormitoryRepository.Create((dormitory));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterGuard([Bind(Include = "PersonalCode,Name,Surname,DormitoryId,Username,Password")] Guard guard)
        {
            ViewBag.DormitoryId = new SelectList(dormitoryRepository.GetAll(), "ID", "Name", guard.DormitoryId);
            ViewBag.DefaultPassword = guard.Password;
            guardRepository.Create(guard);
            return View();
        }

        public ActionResult RegisterGuard()
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
                ViewBag.DormitoryId = Dormitory;
                ViewBag.DefaultPassword = GetRandomAlphaNumeric();
                #endregion
                return View();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRoom([Bind(Include = "DormitoryId,Number")] Room room)
        {
            ViewBag.DormitoryId = new SelectList(dormitoryRepository.GetAll(), "ID", "Name", room.DormitoryId);
            return View();
        }

        public ActionResult CreateRoom()
        {
            if (Session["Username"] != null)
            {
                #region ViewBag
                List<Dormitory> dormitories = dormitoryRepository.GetAll();
                List<SelectListItem> Dormitory = new List<SelectListItem>();
                if (dormitories.Count() == 0)
                {
                    Dormitory.Add(new SelectListItem { Text = "Nėra registruotų bendrabučių", Value = "-1" });
                }
                else
                {
                    Dormitory.Add(new SelectListItem { Text = "", Value = "-1" });
                    for (int i = 0; i < dormitories.Count(); i++)
                    {
                        var temp = new SelectListItem { Text = dormitories[i].Name, Value = dormitories[i].ID.ToString() };
                        Dormitory.Add(temp);
                    }
                }
                ViewBag.DormitoryId = Dormitory;
                #endregion
                return View();
            }
            return RedirectToAction("Index");
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
            return RedirectToAction("Index");
        }

        private string GetRandomAlphaNumeric()
        {
            Random random = new Random();
            var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(chars.Select(c => chars[random.Next(chars.Length)]).Take(8).ToArray());
        }
    }
}