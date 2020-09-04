using Microsoft.AspNetCore.Mvc;
using System.Linq;
using UdlånsWeb.DataHandling;
using UdlånsWeb.DataHandling.DataCheckUp;
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
            UserViewModel userModel = Data.UserData;
            if (userModel == null)
            {
                //If there is no users make an empty list
                userModel = new UserViewModel();
            }
            //returns the user view model
            userModel.Users.Sort();
            return View(userModel);
        }
   
        [HttpGet]
        //This is for user model/view
        public IActionResult AddUser()
        {
            if (CurrentUser.User == null || CurrentUser.User.Admin == false)
                return Redirect("Home/ErrorPage");
            
            //returns the AddUser page 
            return View();
        }
       
        [HttpPost]
        //This is for adding users
        public IActionResult AddUser(User user)
        {
            IdControl.GiveIdToUser(user);
            //Gets the selected user from UserPage 
            //Then sends it to data 
            Data.UserData.Users.Add(user);
            Data.SaveUsers();
            //After they user has been saved redirect to UserPage
            return Redirect("UserPage");
        }


        [HttpGet]
        public IActionResult EditUser(int id)
        {
            if (CurrentUser.User == null || CurrentUser.User.Admin == false)
                return Redirect("Home/ErrorPage");
            Data.GetUsers();
            User userToEdit = Data.UserData.Users.Where(x => x.Id == id).FirstOrDefault();

            return View(userToEdit);
        }
        [HttpPost]
        public IActionResult EditUser(User user)
        {
            Data.EditUser(user);
            return Redirect("UserPage");
        }

        [HttpGet]
        public IActionResult DeleteUser(int id)
        {
            if (CurrentUser.User == null || CurrentUser.User.Admin == false)
                return Redirect("Home/ErrorPage");
            Data.GetUsers();
            User userToDelete = Data.UserData.Users.Where(x => x.Id == id).FirstOrDefault();

            //Sends the selected user to the delete view
            return View(userToDelete);
        }
        [HttpPost]
        public IActionResult DeleteUser(User user)
        {
            Data.DeleteUser(user);
            return Redirect("UserPage");
        }

    }
}
