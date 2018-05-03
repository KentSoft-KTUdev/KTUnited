using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataContract.Data;
using DataContract.Objects;

namespace WebAPI.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Index()
         {
            return View();
        }  
    }
}