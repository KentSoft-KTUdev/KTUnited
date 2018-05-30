using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DataContract.Data;
using DataContract.Objects;
using DataContract;

namespace WebAPI.Controllers
{
    public class MainController : Controller
    {
        private AdministratorRepository administratorRepository = new AdministratorRepository();
        private ResidentRepository residentRepository = new ResidentRepository();
        private GuardRepository guardRepository = new GuardRepository();
        // GET: Main
        public ActionResult Index()
        {
            Random random = new Random(DateTime.Now.Millisecond);
            Administrator testAdmin = new Administrator
            {
                DormitoryId = 2,
                Name = "ddx",
                Password = "dasda",
                PersonalCode = random.Next(),
                Surname = "äsdad",
                Username = "asdasd"
            };
            administratorRepository.Create(testAdmin);
            return View();
        }

        public ActionResult LoginForm()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Username,Password")]string username, string password)
        {
            if (administratorRepository.Login(username, Configuration.Encryption(password)).IsSuccessStatusCode)
            {
                Session["Username"] = username;
                Session["UserID"] = administratorRepository.ReadByUsername(username).PersonalCode;
                Session["AccessLevel"] = "Administrator";
                return RedirectToAction("Index", "Admin");
            }
            else if (guardRepository.Login(username, Configuration.Encryption(password)).IsSuccessStatusCode)
            {
                Session["Username"] = username;
                Session["UserID"] = guardRepository.ReadByUsername(username).PersonalCode;
                Session["AccessLevel"] = "Guard";
                return RedirectToAction("Index", "Guard");
            }
            else if (residentRepository.Login(username, Configuration.Encryption(password)).IsSuccessStatusCode)
            {
                Session["Username"] = username;
                Session["UserID"] = residentRepository.ReadByUsername(username).PersonalCode;
                Session["AccessLevel"] = "Resident";
                return RedirectToAction("Index", "Resident");
            }
            else
            {
                return RedirectToAction("LoginForm");
                //nepaejo prisijungti vairantas
            }
        }
    }
}