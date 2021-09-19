using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using sb_light_admin.view.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace sb_light_admin.view.Controllers
{

    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            //here we can write our code and this code will only be seen as local variable inside this view only
            // but in case you want to RedirectToView(), you can use TempData[].
            ViewData["anyVariable"] = "this is a variable of type ViewData";
            ViewBag.anyVariableTwo = "this is a variable of type ViewBag";
            TempData["anyVariableThree"] = "this is a vairable of type TempData";

            return View();
        }



        [HttpGet]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        public IActionResult Privacy()
        {

            //note that ViewData and TempData are the same as Object in c#
            // while ViewBag is like Dynamic in C#

            //first, let's make new objects from the class pageadmins and add prop. to each object :

            PageAdmins newPAdmin1 = new PageAdmins() { Id = 3, Name = "Hassan", IsAuthorized = true };
            PageAdmins newPAdmin2 = new PageAdmins() { Id = 6, Name = "Ali", IsAuthorized = false };
            PageAdmins newPAdmin3 = new PageAdmins() { Id = 9, Name = "Nasser", IsAuthorized = true };

            //second, let's make a list to combine all the introduced objects into newlist :

            List<PageAdmins> newAdminTable = new List<PageAdmins>()
            {
                newPAdmin1,
                newPAdmin2,
                newPAdmin3
            };

            ViewBag.tableOfAdmins = newAdminTable;


            var count = 1;
            ViewBag.Counter += count;




            return View(ViewBag.tableOfAdmins);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
