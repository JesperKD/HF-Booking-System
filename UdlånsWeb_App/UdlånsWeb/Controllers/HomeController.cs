using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
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
        private ToTxt ToTxt = new ToTxt();
        private FromTxt FromTxt = new FromTxt();
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

            ////testing
            //User u = new User();
            //u.Name = "Kage Mand";
            //u.Initials = "KM";
            //u.Email = "kage@testing.dk";
            //u.Admin = false;
            //AddUser(u);
            ////testing

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
            var userModel = new UserViewModel();

            string[] rawUser = FromTxt.StringsFromTxt(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\user.txt");
            userModel.Users = ConvertData.ConverDataToUser(rawUser);
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
            //Save the user to file/database

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(user.Name + "," + user.Initials + "," + user.Email + "," + user.Admin);

            // change to correct path for file saving
            ToTxt.AppendStringToTxt(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\user.txt", stringBuilder.ToString() + Environment.NewLine);
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

        [HttpGet]
        public IActionResult EditUser(UserViewModel userList, int id)
        {
            return View(SelectedUserForEdit);
        }
        [HttpPost]
        public IActionResult EditUser(User user)
        {
            //Logic for Edit User


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
