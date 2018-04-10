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
            AdministratorRepository rep = new AdministratorRepository();
            Administrator administrator = new Administrator();
            administrator.Name = "test";
            administrator.Surname = "xdd";
            administrator.PersonalCode = 456464;
            rep.Create(administrator);
            administrator.Name = "xd";
            administrator.PersonalCode = 477774;
            administrator.Surname = "xdd";
            rep.Create(administrator);
            List<Administrator> admins = rep.GetAll();
            Administrator temp = rep.Read(456464);
            return View();
        }  
    }
}