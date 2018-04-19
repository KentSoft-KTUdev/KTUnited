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
            return View();
        }

        public ActionResult RegisterGuard() {
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
            return View();
        }
    }
}