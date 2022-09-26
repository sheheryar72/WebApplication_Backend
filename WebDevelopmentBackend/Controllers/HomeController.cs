using System.Data.SqlClient;
using System.Web.Mvc;
using WebDevelopmentBackend.Models;

namespace WebDevelopmentBackend.Controllers
{
    public class HomeController : Controller
    {
        // Dashboard
        public ActionResult Index()
        {
            if(Session["IsUserLoggedIn"] == null)
            {
                return RedirectToAction("Login");
            } else
            {
                ViewBag.Username = Session["Username"];

                return View();
            }            
        }

        // Login Page
        public ActionResult Login()
        {
            if(Session["IsUserLoggedIn"] != null)
            {
                return RedirectToAction("Index");
            } else
            {
                return View();
            }
        }

        // Authentication of Login Page
        [HttpPost]
        public ActionResult AuthenticateUser(FormCollection form)
        {
            string Email = form.Get("email");
            string Password = form.Get("password");

            if (Email != null && Password != null)
            {
                UsersManager usersManager = new UsersManager();

                bool IsUserAuthenticated = usersManager.AuthenticateUser(Email, Password);


                if (IsUserAuthenticated)
                {
                    Session["IsUserLoggedIn"] = true;
                    Session["Email"] = Email;

                    return RedirectToAction("Index");
                } else
                {
                    TempData["Error"] = "Email or password is incorrect, please try again.";

                    return RedirectToAction("Login");
                }

            } else
            {
                TempData["Error"] = "Email and password must not be empty."; 

                return RedirectToAction("Login");
            }

        }

        // Logout
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Login");
        }

        public ActionResult Signup()
        {
            if(Session["IsUserLoggedIn"] != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();  
        }

        //[HttpPost]
        //public ActionResult CreateUserUsingFormCollection(FormCollection form)
        //{
        //    string email = form.Get("email");
        //    string password = form.Get("password");
        //    string name = form.Get("name");
        //    string dob = form.Get("dob");
        //    string gender = form.Get("gender");

        //    if (email != null &&
        //       password != null &&
        //       name != null &&
        //       dob != null &&
        //       gender != null)
        //    {
        //        //TODO: Save user in DB



        //    }

        //}

        [HttpPost]
        public ActionResult CreateUser(User user)
        {
            // Automatic model binding
            // FormCollection

            if(ModelState.IsValid)
            {
                UsersManager usersManager = new UsersManager();
                usersManager.AddUserInDB(user);

                TempData["Message"] = "You have signed up successfully";

                return RedirectToAction("Login", "Home");
            }

            TempData["Error"] = "Form values incorrect";

            return RedirectToAction("Signup", "Home");


            //string email = form.Get("email");
            //string password = form.Get("password");
            //string name = form.Get("name");
            //string dob = form.Get("dob");
            //string gender = form.Get("gender");

            //if(email != null &&
            //   password != null &&
            //   name != null &&
            //   dob != null &&
            //   gender != null)
            //{
            // TODO: Save user in DB

            //}

        }
    }
}