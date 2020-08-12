using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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

        #region HomePage
        [HttpGet]
        public IActionResult HomePage()
        {
            var ItemModel = new List<Item>();
            ItemModel = TestData.GetItems(); 
            return View();
        }
        [HttpPost]
        public IActionResult HomePage(string initials)
        {
            //Add logic for login 


            //Redirect to InfoPage
            return Redirect("/Home/InfoPage");
        }
        #endregion

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult AdminSite()
        {
            var ItemModel = new List<Item>();
            ItemModel = TestData.GetItems();
            return View(ItemModel);
        }
        [HttpGet]
        public IActionResult InfoPage()
        {
            var ItemModel = new List<Item>();
            ItemModel = TestData.GetItems();
            return View(ItemModel);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult InfoPage(Item item)
        {

            return Redirect("/Home");
        }

        //Used to see the users
        #region UserPage
        [HttpGet]
        public IActionResult UserPage()
        {
            var UserModel = new List<User>();
            return View(UserModel);
        }

        [HttpPost]
        public IActionResult UserPage(User user)
        {
            return View();
        }
        #endregion

        //This is for adding users
        #region UserControl
        private User createdUser;
        [HttpPost]
        public IActionResult AddUser(User user)
        {
            

            //Save the user to file/database
            createdUser = user;
            //When the user clicks sava they will be returned to the userpage
            return Redirect("/Home/UserPage");
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            //returns the AddUser page 
            return View();
        }
        #endregion

        #region Edit User
        public IActionResult EditUser()
        {
            return View();
        }
        public IActionResult EditUser(int? id)
        {
            return View();
        }
        #endregion
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
