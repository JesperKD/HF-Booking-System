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
        private ConvertCourseData convertCourseData = new ConvertCourseData();
        private ConvertItemData convertItemData = new ConvertItemData();
        private ConvertUserData convertUserData = new ConvertUserData();
        private ConvertLoginData convertlogindata = new ConvertLoginData();


        private static User SelectedUser { get; set; }
        private static Item SelectedItem { get; set; }
        private static BookingViewModel bookingViewModel { get; set; }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        #region HomePage - Login page
        [HttpGet]
        public IActionResult HomePage()
        {
            SelectedUser = convertlogindata.AutoLogin();

            // remove to prevent any logins
            // insert a user not found message
            if (SelectedUser == null)
                SelectedUser = new User();

            if (SelectedUser.Admin == true)
                return Redirect("HomePage");

            else
                return Redirect("Home/Booking");
        }

        [HttpPost]
        public IActionResult HomePage(string initials)
        {
            //Add logic for login
            convertlogindata.CheckLogin(initials);

            //Redirect to InfoPage
            return Redirect("/Home/InfoPage");
        }
        #endregion
        [HttpGet]
        public IActionResult Booking()
        {
            return View();
        }
        public IActionResult Booking(Course course)
        {
            bookingViewModel = new BookingViewModel
            {

                CourseModel = new Course()
                {
                    Defined = course.Defined
                },

                HostRentedForCourse = new Item()
                {
                    TurnInDate = DateTime.Now.Date,
                    RentedDate = DateTime.Now.Date
                },


            };
            try
            {
                bookingViewModel.CoursesForSelection = convertCourseData.GetCourses().Courses;
            }
            catch (Exception e)
            {


            }
            finally
            {
                bookingViewModel.CoursesForSelection = new List<Course>();
            }

            return Redirect("InfoPage");
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
            ItemViewModel itemModel = convertItemData.GetItems();
            if (itemModel == null)
            {
                itemModel = new ItemViewModel();
            }
            return View(itemModel);
        }

        [HttpPost]
        public IActionResult AdminSite(string searchInput)
        {
            return View();
        }


        [HttpGet]
        public IActionResult InfoPage()
        {
            return View(bookingViewModel);
        }

        [HttpPost]
        public IActionResult InfoPage(BookingViewModel booking)
        {
            //Make a booking save file


            return Redirect("/Home");
        }

        //Overview over all user pages
        #region User Pages


        [HttpGet]
        public IActionResult UserPage()
        {
            UserViewModel userModel = convertUserData.GetUsers();
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


        //This is for adding users


        [HttpPost]
        public IActionResult AddUser(User user)
        {
            convertUserData.AddUser(user);
            return Redirect("/Home/UserPage");
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            //returns the AddUser page 
            return View();
        }

        [HttpGet]
        public IActionResult EditUser(UserViewModel userList, int id)
        {
            return View(SelectedUser);
        }
        [HttpPost]
        public IActionResult EditUser(User user)
        {
            convertUserData.EditUser(user);
            return Redirect("UserPage");
        }


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
            convertUserData.DeleteUser(user);
            return Redirect("UserPage");
        }


        #endregion

        //Overview over all item pages
        #region Item Pages

        #region Add Item
        [HttpGet]
        public IActionResult AddItem()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddItem(Item item)
        {
            convertItemData.AddItem(item);
            return Redirect("AdminSite");
        }
        #endregion

        #region Edit Item
        [HttpGet]
        public IActionResult EditItem(ItemViewModel item, int id)
        {


            return View(item.Items[id]);
        }

        [HttpPost]
        public IActionResult EditItem(Item item)
        {
            convertItemData.EditItem(item);
            return Redirect("AdminSite");
        }
        #endregion

        #region Delete Item 
        [HttpGet]
        public IActionResult DeleteItem(ItemViewModel item, int id)
        {
            SelectedItem = item.Items[id];
            return View(SelectedItem);
        }

        [HttpPost]
        public IActionResult DeleteItem(Item item)
        {
            convertItemData.DeleteItem(item);
            return Redirect("AdminSite");
        }
        #endregion
        #endregion

        //Overview over all course pages
        #region Course Pages

        public IActionResult CourseSite()
        {
            CourseViewModel viewModel = new CourseViewModel();
            viewModel = convertCourseData.GetCourses();
            if (viewModel == null)
                return View(new CourseViewModel());
            else
                return View(viewModel);
        }


        #region Add Course
        [HttpGet]
        public IActionResult AddCourse()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCourse(Course course)
        {
            convertCourseData.AddCourse(course);
            return Redirect("CourseSite");
        }
        #endregion

        #region Edit Course
        [HttpGet]
        public IActionResult EditCourse(CourseViewModel course, int id)
        {
            return View(course.Courses[id]);
        }

        [HttpPost]
        public IActionResult EditCourse(Course course)
        {
            convertCourseData.EditCourse(course);
            return Redirect("CourseSite");
        }
        #endregion

        #region Delete Course
        [HttpGet]
        public IActionResult DeleteCourse(CourseViewModel course, int id)
        {
            bookingViewModel.CourseModel = course.Courses[id];
            return View(course.Courses[id]);
        }

        [HttpPost]
        public IActionResult DeleteCourse(Course course)
        {
            convertCourseData.DeleteCourse(course);
            return Redirect("CourseSite");
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
