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
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IClassManager classManager;
        private readonly IUserManager userManager;
        private readonly IUserClassManager userClassManager;

        public HomeController(ILogger<HomeController> logger, 
                              IClassManager ClassManagerInstance,
                              IUserManager UserManagerInstance,
                              IUserClassManager UserClassManager)
        //public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            classManager = ClassManagerInstance;
            userManager = UserManagerInstance;
            userClassManager = UserClassManager;
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult EnrollInClass()
        {
            EnrollViewModel classList = new EnrollViewModel();
            classList.Classes = classManager.Classes
                .Select(c => new ItemList()
                    {
                        Text = c.ClassName,
                        Value = c.ClassId
                    })
                .ToList<ItemList>();
            return View(classList);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult EnrollInClass(EnrollViewModel ChosenClass)
        {
            if (ModelState.IsValid == true)
            {
                Business.UserModel user = userManager.User(User.Identity.Name);
                userClassManager.RegisterForClass(user.UserId, ChosenClass.SelectedClass);
                return Redirect("~/Home/StudentClasses");
            }

            return View();
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

        [Authorize(Roles = "User")]
        public IActionResult StudentClasses()
        {
            Business.UserModel user = userManager.User(User.Identity.Name);

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

            // Store the new user
            userManager.RegisterUser(UserRegistration.Email, UserRegistration.Password);

            //Complete Registration
            return View("Registered", UserRegistration);
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewData["ReturnUrl"] = Request.Query["returnUrl"];
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserLogin LoginInfo, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                bool isValidLogin = userManager.LoginUser(LoginInfo.UserEmail, LoginInfo.UserPassword);

                if (isValidLogin)
                {
                    string jsonLogin = JsonSerializer.Serialize(new Models.UserModel()
                    {
                        UserEmail = LoginInfo.UserEmail
                    });

                    HttpContext.Session.SetString("User", jsonLogin);

                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, LoginInfo.UserEmail),
                    new Claim(ClaimTypes.Role, "User"),
                };

                    var claimsIdentity = new ClaimsIdentity(claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = false,
                        // Refreshing the authentication session should be allowed.

                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                        // The time at which the authentication ticket expires. A 
                        // value set here overrides the ExpireTimeSpan option of 
                        // CookieAuthenticationOptions set with AddCookie.

                        IsPersistent = false,
                        // Whether the authentication session is persisted across 
                        // multiple requests. When used with cookies, controls
                        // whether the cookie's lifetime is absolute (matching the
                        // lifetime of the authentication ticket) or session-based.

                        IssuedUtc = DateTimeOffset.UtcNow,
                        // The time at which the authentication ticket was issued.
                    };

                    HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        claimsPrincipal,
                        authProperties).Wait();

                    return Redirect(returnUrl ?? "~/");
                }

                ModelState.AddModelError("UserPassword", "Error: User name and password do not match");
                return View(LoginInfo);
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(LoginInfo);
        }

        [Authorize(Roles = "User")]
        public ActionResult Logout()
        {
            HttpContext.Session.Remove("User");

            HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("~/");
        }
    }
}
