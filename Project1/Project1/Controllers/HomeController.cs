using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project1.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Project1.Business;

namespace Project1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IClassManager classManager;

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
    }
}
