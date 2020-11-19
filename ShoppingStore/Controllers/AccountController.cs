using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ShoppingStore.Areas.Admin;
using ShoppingStore.Models;
using ShoppingStore.Entity;
using ShoppingStore.BL;
using AutoMapper;

namespace ShoppingStore.Controllers
{
    public class AccountController : Controller
    {
        static readonly log4net.ILog log =

       log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        IAccountBL accountBL;
        public AccountController()
        {
            accountBL = new AccountBL();
        }
        public ActionResult Index()
        {
            return Redirect("~/account/login");
        }
        public ActionResult Login()
        {
            try
            {
                string user = User.Identity.Name;
                if (!string.IsNullOrEmpty(user))
                    return RedirectToAction("user-profile");
            }
            catch(Exception exception)
            {
                log.Error(exception.Message);
            }
            return View();
        }
        [HttpGet]
        [ActionName("create-account")]
        public ActionResult CreateAccount()
        {
            return View("CreateAccount");
        }
        // POST: /account/login
        [HttpPost]
        public ActionResult Login(LoginUserViewModel model)
        {
            Login fetchedData = null;
            try
            {
                // Check model state
                if (!ModelState.IsValid)
                {
                    var config = new MapperConfiguration(cfg => { cfg.CreateMap<Login, LoginUserViewModel>(); });
                    IMapper mapper = config.CreateMapper();
                    LoginUserViewModel sensitiveData = mapper.Map<Login, LoginUserViewModel>(fetchedData);

                    return View(sensitiveData);
                }
            }
            catch (Exception exception)
            {
                log.Error(exception.Message);
            }

            // Check if the user is valid

            bool isValid = accountBL.Login(fetchedData);

                if (!isValid)
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                    return View(fetchedData);
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe);
                    return Redirect(FormsAuthentication.GetRedirectUrl(model.Username, model.RememberMe));
                }
            

         

        }
        //POST: /account/create-account
        [HttpPost]
        [ActionName("create-account")]

        public ActionResult CreateAccount(UserViewModel model)
        {
            User fetchedData = null;
            // Check model state
            try
            {
                if (!ModelState.IsValid)
                {
                    var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserViewModel>(); });
                    IMapper mapper = config.CreateMapper();
                    UserViewModel sensitiveData = mapper.Map<User, UserViewModel>(fetchedData);
                    return View("CreateAccount", fetchedData);
                }

            }
            catch(Exception exception)
            {
                log.Error(exception.Message);
            }
            if (accountBL.CreateAccount(fetchedData))
            {
                ModelState.AddModelError("", "Username " + model.UserName + " is taken.");
                return View("CreateAccount", model);

            }

            // Create a TempData message
            TempData["SM"] = "You are now registered and can login.";

            // Redirect
            return Redirect("~/account/login");
        }

        // GET: /account/Logout
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("~/account/login");
        }
        public ActionResult ShowUserNameInNav()
        {
            ShowUserViewModel model = null;

            try
            {
                string username = User.Identity.Name;
                // Declare model

                User user = accountBL.ShowUser(username);
                model = new ShowUserViewModel()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
            }
            catch (Exception exception)
            {
                log.Error(exception.Message);
            }

            return PartialView(model);
        }
        //GET:/account/UserProfile
        [HttpGet]
        [ActionName("user-profile")]
        public ActionResult UserProfile()
        {
            string username = User.Identity.Name;
            UserProfileViewModel sensitiveData = null;
            try
            {
               
                User user = accountBL.UserProfile(username);
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserProfileViewModel>(); });
                IMapper mapper = config.CreateMapper();
                sensitiveData = mapper.Map<User, UserProfileViewModel>(user);
            }
            catch(Exception exception)
            {
                log.Error(exception.Message);
            }
            return View("UserProfile", sensitiveData);
        }

    

    }
}
    