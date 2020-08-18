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
            //Test fremvisning af filepath på privacy page (slet efter test)
            ViewData["filepath"] = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);


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
            var model = new ItemViewModel();
            model.Items = TestData.GetItems();
            return View(model);
        }
        [HttpPost]
        public IActionResult InfoPage(ItemViewModel item, int? id)
        {
            return Redirect("/Home");
        }

        //Used to see the users
        #region UserPage
        [HttpGet]
        public IActionResult UserPage()
        {
            UserViewModel userModel = ConvertData.GetUsers();
            if (userModel.Users.Count() == 0) return NotFound(StatusCodes.Status404NotFound);
            return View(userModel);
        }

        //Need to find a way around this
        public static User SelectedUserForEdit { get; set; }

        [HttpPost]
        public IActionResult UserPage(UserViewModel user, int id)
        {
            SelectedUserForEdit = user.Users[id];
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
            return View(SelectedUserForEdit);
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
            SelectedUserForEdit = user.Users[id];
            return View(SelectedUserForEdit);
        }
        [HttpPost]
        public IActionResult DeleteUser(User user)
        {
            ConvertData.DeleteUser(user);
            return Redirect("UserPage");
        }

        #endregion
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
