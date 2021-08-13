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

        public HomeController(ILogger<HomeController> logger, IClassManager ClassManagerInstance)
        //public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            classManager = ClassManagerInstance;
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
            var userName = HttpContext.User.Identity.Name;
            Project1.Models.UserModel userInfo = (Project1.Models.UserModel)userManager.Users
                .Where(n => userName == n.UserEmail)
                .Select(u => new Project1.Models.UserModel()
                {
                    UserId = u.UserId,
                    UserEmail = u.UserEmail,
                    UserIsAdmin = u.UserIsAdmin,
                    UserPassword = u.UserPassword
                });

            ClassViewModel model = new ClassViewModel()
                {
                Classes = classManager.Classes
                    //.Where(c  => userInfo.UserId == c.
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
            if (ModelState.IsValid)
            {
                //Complete Registration
                return View("Registered", UserRegistration);
            }
            else
            {
                return View(UserRegistration);
            }
        }
    }
}
