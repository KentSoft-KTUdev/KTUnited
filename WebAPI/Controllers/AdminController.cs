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

        public ActionResult RegisterDormitory() {
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