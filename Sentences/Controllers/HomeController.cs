using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace Sentences.Controllers
{
    public class HomeController : Controller
    {
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
