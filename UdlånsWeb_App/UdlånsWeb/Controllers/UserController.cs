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
    public class UserController : Controller
    {
        //User overview
        public IActionResult UserPage()
        {
            //Checks if user is addmin or if they have a user 
            //if not they get redirected to a error page
            if (CurrentUser.User == null || CurrentUser.User.Admin == false)
                return Redirect("Home/ErrorPage");

            //Gets the list of users 
            UserViewModel userModel = Data.UserViewModel;
            if (userModel == null)
            {
                //If there is no users make an empty list
                userModel = new UserViewModel();
            }
            //returns the user view model
            return View(userModel);
        }
   
        [HttpGet]
        //This is for user model/view
        public IActionResult AddUser()
        {
            if (CurrentUser.User == null && CurrentUser.User.Admin == true)
                return Redirect("Home/ErrorPage");

            //returns the AddUser page 
            return View();
        }
       
        [HttpPost]
        //This is for adding users
        public IActionResult AddUser(User user)
        {
            //Gets the selected user from UserPage 
            //Then sends it to data 
            Data.UserViewModel.Users.Add(user);
            //After they user has been saved redirect to UserPage
            return Redirect("UserPage");
        }


        [HttpGet]
        public IActionResult EditUser(UserViewModel userViewModel, int id)
        {
            if (CurrentUser.User == null || CurrentUser.User.Admin == false)
                return Redirect("Home/ErrorPage");

            return View(userViewModel.Users[id]);
        }
        [HttpPost]
        public IActionResult EditUser(User user)
        {
            Data.ConvertUserData.EditUser(user);
            return Redirect("UserPage");
        }


        [HttpGet]
        public IActionResult DeleteUser(UserViewModel userViewModel, int id)
        {
            if (CurrentUser.User == null || CurrentUser.User.Admin == false)
                return Redirect("Home/ErrorPage");

            //Sends the selected user to the delete view
            return View(userViewModel.Users[id]);
        }
        [HttpPost]
        public IActionResult DeleteUser(User user)
        {
            Data.ConvertUserData.DeleteUser(user);
            return Redirect("UserPage");
        }

    }
}
