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
            //u.Id = 1;
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

            // rewrite to handle decryption
            string[] rawUser = FromTxt.StringsFromTxt(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\user.txt");

            foreach (string userLine in rawUser)
            {
                string[] userData = userLine.Split(',');
                Models.User user = new User();
                user.Name = userData[0];
                user.Initials = userData[1];
                user.Email = userData[2];
                user.Admin = Convert.ToBoolean(userData[3]);
                user.Id = int.Parse(userData[4]);
                userModel.Users.Add(user);
            }

            return View(userModel);
        }
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

            // rewrite to handle encryption
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(user.Name + "," + user.Initials + "," + user.Email + "," + user.Admin + "," + user.Id);

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

            var userModelOld = new UserViewModel();

            // gets all users from file
            string[] rawUser = FromTxt.StringsFromTxt(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\user.txt");

            foreach (string userLine in rawUser)
            {
                string[] userData = userLine.Split(',');
                Models.User oUser = new User();
                oUser.Name = userData[0];
                oUser.Initials = userData[1];
                oUser.Email = userData[2];
                oUser.Admin = Convert.ToBoolean(userData[3]);
                oUser.Id = int.Parse(userData[4]);
                userModelOld.Users.Add(oUser);
            }

            // finds the old user and removes it
            User OldUser = userModelOld.Users.Where(x => x.Id == user.Id).FirstOrDefault();
            userModelOld.Users.Remove(OldUser);

            // creates new list from old, and inserts edited user at index Id
            UserViewModel userModelNew = new UserViewModel();
            userModelNew = userModelOld;
            userModelNew.Users.Insert(user.Id, user);

            // creates correct user string
            List<string> usersTosave = new List<string>();

            // makes each user into a new string
            foreach (User Item in userModelNew.Users)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(Item.Name + "," + Item.Initials + "," + Item.Email + "," + Item.Admin + "," + Item.Id);

                usersTosave.Add(stringBuilder.ToString());
                // change to correct path for file saving
            }
            // overrides file with new strings
            ToTxt.StringsToTxt(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\user.txt", usersTosave.ToArray());

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
