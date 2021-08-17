using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project1.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Project1.Business;
using Microsoft.AspNetCore.Authorization;

namespace Project1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IClassManager classManager;
        private readonly IUserManager userManager;

        public HomeController(ILogger<HomeController> logger, 
                              IClassManager ClassManagerInstance,
                              IUserManager UserManagerInstance)
        //public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            classManager = ClassManagerInstance;
            userManager = UserManagerInstance;
        }

        public IActionResult Classes()
        {
            ClassViewModel model = new ClassViewModel()
            {
                Classes = classManager.Classes
                .Select(t => new Project1.Models.ClassModel()
                {
                    ClassId = t.ClassId,
                    ClassDescription = t.ClassDescription,
                    ClassName = t.ClassName,
                    ClassPrice = t.ClassPrice
                })
                .ToArray()
            };

            return View(model);
        }

        //[Authorize]
        public IActionResult StudentClasses()
        {
            int UserId = 2;
            Business.UserModel user = userManager.User(UserId);

            Models.UserModel userModel = new Models.UserModel()
            {
                UserId = user.UserId,
                UserEmail = user.UserEmail,
                UserIsAdmin = user.UserIsAdmin,
                UserPassword = user.UserPassword,
                Classes = user.Classes
                    .Select(c => new Models.ClassModel()
                    {
                        ClassId = c.ClassId,
                        ClassName = c.ClassName,
                        ClassDescription = c.ClassDescription,
                        ClassPrice = c.ClassPrice
                    })
                    .ToList<Models.ClassModel>()
            };

            return View(userModel);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Registration UserRegistration)
        {
            if (ModelState.IsValid == false)
            {
                return View(UserRegistration);
            }

            //Check if user is already registered
            var user = userManager.User(UserRegistration.Email);
            if (user != null)
            {
                ModelState.AddModelError("Email", "Error: User registration already exists");
                return View(UserRegistration);
            }

            if (UserRegistration.Password != UserRegistration.Password2)
            {
                ModelState.AddModelError("Password2", "Error: Passwords do not match");
                return View(UserRegistration);
            }

            // Store the new user
            userManager.RegisterUser(UserRegistration.Email, UserRegistration.Password);

            //Complete Registration
            return View("Registered", UserRegistration);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserLogin LoginInfo)
        {
            if (ModelState.IsValid)
            {
                return View();
            }
            else
            {
                return View(LoginInfo);
            }
        }
    }
}
