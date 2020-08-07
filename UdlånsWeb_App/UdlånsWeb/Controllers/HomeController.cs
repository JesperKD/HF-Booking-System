using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
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
        public IActionResult HomePage()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult AdminSite()
        {
            ItemList itemList = new ItemList();
            itemList.Items = TestData.GetItems();
            return View(itemList);
        }
        public IActionResult InfoPage()
        {
            ItemList itemList = new ItemList();
            itemList.Items = TestData.GetItems();
            return View(itemList);
        }


        //Used to see the users
        #region UserPage
        [HttpGet]
        public IActionResult UserPage()
        {
            UserList users = new UserList();
            users.Users = TestData.GetUsers();
            return View(users);
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
            createdUser = user;



            return Redirect("/Home/UserPage");
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }
        #endregion

        [HttpPost]
        public IActionResult EditUser()
        {
            return View();
        }
        [HttpGet]
        public IActionResult EditUser(int? id)
        {
            UserList userList = new UserList();
            userList.Users = TestData.GetUsers();

            return View();
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
