﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using UdlånsWeb.DataHandling;
using UdlånsWeb.Models;

namespace UdlånsWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult HomePage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult HomePage(string initials)
        {
            Data.GetUsers();
            CurrentUser.User = Data.GetUsers().Users.Where(x => x.Initials == initials).FirstOrDefault();
            
            //Login still needs some work
            if (CurrentUser.User == null)
                return Redirect("Home/ErrorPage");

            if (CurrentUser.User.Admin == true)
                return Redirect("/Item/AdminSite");

            else
                return Redirect("Booking/BookingDefine");
        }
       
        public IActionResult Privacy()
        {
            var ItemModel = new HostViewModel();
            ItemModel = Data.GetHosts();
            return View(ItemModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ErrorPage()
        {
            return View(new ErrorViewModel());
        }
    }
}
