using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using UdlånsWeb.DataHandling;
using UdlånsWeb.Models;

namespace UdlånsWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ToTxt ToTxt = new ToTxt();
        private FromTxt FromTxt = new FromTxt();
        private ConvertData ConvertData = new ConvertData();
        private static Course Course { get; set; } 
        private static User SelectedUser{ get; set; }
        private static Item SelectedItem { get; set; }
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        #region HomePage - Login page
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
        [HttpGet]
        public IActionResult Booking()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Booking(Course course)
        {
            Course = course;
            //this will return a booking view with a start date and end date
            if (course.Difined == true)
            {
                return Redirect("InfoPage");
            }
            //This will only have a start date
            else
            {
                return InfoPage();
            }
        }

        public IActionResult Privacy()
        {
            var ItemModel = new ItemViewModel();
            ItemModel.Items = TestData.GetItems();
            return View(ItemModel);
        }

        [HttpGet]
        public IActionResult AdminSite()
        {
            var ItemModel = new ItemViewModel();
            ItemModel.Items = TestData.GetItems();
            return View(ItemModel);
        }

        [HttpPost]
        public IActionResult AdminSite(string searchInput)
        {
            // make some logic to filter out hosts from list
            return View();
        }

        //All pages with a itemview
        #region Item Pages

        [HttpGet]
        public IActionResult InfoPage()
        {
            return View(new Course());
        }

        [HttpPost]
        public IActionResult InfoPage(Course course, int? id)
        {
            return Redirect("/Home");
        }

        [HttpGet]
        public IActionResult AddItem()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddItem(Item item)
        {
            SelectedItem = item;
            return Redirect("AdminSite");
        }

        [HttpGet]
        public IActionResult EditItem(ItemViewModel item, int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult EditItem()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DeleteItem(ItemViewModel item, int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteItem(Item item)
        {
            return View();
        }
        #endregion


        //Overview over all user pages
        #region User Pages

        #region UserPage
        [HttpGet]
        public IActionResult UserPage()
        {
            UserViewModel userModel = ConvertData.GetUsers();
            if (userModel == null)
            {
                userModel = new UserViewModel();
            }
            return View(userModel);
        }

        //Need to find a way around this
        [HttpPost]
        public IActionResult UserPage(UserViewModel user, int id)
        {
            SelectedUser = user.Users[id];
            return Redirect("EditUser");
        }
        #endregion

        //This is for adding users
        #region UserControl

        [HttpPost]
        public IActionResult AddUser(User user)
        {
            ConvertData.AddUser(user);
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

        [HttpGet]
        public IActionResult EditUser(UserViewModel userList, int id)
        {
            return View(SelectedUser);
        }
        [HttpPost]
        public IActionResult EditUser(User user)
        {
            ConvertData.EditUser(user);
            return Redirect("UserPage");
        }
        #endregion

        #region Delete User
        [HttpGet]
        public IActionResult DeleteUser(UserViewModel user, int id)
        {
            //Sends the right user to the delete view
            SelectedUser = user.Users[id];
            return View(SelectedUser);
        }
        [HttpPost]
        public IActionResult DeleteUser(User user)
        {
            ConvertData.DeleteUser(user);
            return Redirect("UserPage");
        }

        #endregion
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
