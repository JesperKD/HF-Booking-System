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
<<<<<<< Updated upstream
        private ToTxt ToTxt = new ToTxt();
        private FromTxt FromTxt = new FromTxt();
        private ConvertData ConvertData = new ConvertData();
        private static Course Course { get; set; } 
        private static User SelectedUser{ get; set; }
        private static Item SelectedItem { get; set; }
=======
        Data Data = new Data();
        private static User SelectedUser { get; set; }
        private static User CurrentUser { get; set; }
        private static Item SelectedItem { get; set; }
        private static BookingViewModel bookingViewModel { get; set; }
        private static BookingViewModel userBooking { get; set; }


>>>>>>> Stashed changes
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

<<<<<<< Updated upstream
        #region Course Pages

        public IActionResult AddCourse()
        {
            return View();
        }

        public IActionResult EditCourse()
=======
        #region HomePage - Login page
        [HttpGet]
        public IActionResult HomePage()
>>>>>>> Stashed changes
        {
            return View();
        }

        public IActionResult DeleteCourse()
        {
            return View();
        }
        #endregion

        #region HomePage - Login page
        [HttpGet]
        public IActionResult HomePage()
        {
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
<<<<<<< Updated upstream


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
            return Redirect("InfoPage");
        }
=======
            CurrentUser = Data.Convertlogindata.ManuelLogin(initials);

            if (CurrentUser == null)
                return Redirect("/Home/ErrorPage");

            if (CurrentUser.Admin == true)
                return Redirect("/Home/AdminSite");

            else
                return Redirect("Home/Booking");
        }
        #region Booking
        [HttpGet]
        public IActionResult InfoPage()
        {
            if (CurrentUser == null)
                return Redirect("ErrorPage");

            return View(bookingViewModel);
        }

        [HttpPost]
        public IActionResult InfoPage(BookingViewModel booking)
        {
            //Make a booking save file
            List<Item> hosts = Data.ConvertItemData.GetItems().Items;
            List<Course> courses = Data.ConvertCourseData.GetCourses().Courses;

            foreach (var item in hosts)
            {
                if (item.NumberOfPeoplePerHost >= booking.CourseModel.NumberOfStudents && item.Rented == false)
                {

                    //sets turnindate to day it was rented plus days its rented for aka turnindate
                    foreach (var course in courses)
                    {
                        if (booking.CourseModel.Name == course.Name)
                        {
                            booking.Id = item.Id;
                        }
                    }

                    booking.HostRentedForCourse = item;
                    //booking.RentedClient = convertlogindata.AutoLogin().Initials;

                    break;
                }
            }
            userBooking = booking;
            return Redirect("ConfirmBooking");
        }
        [HttpGet]
        public IActionResult Booking()
        {
            if (CurrentUser == null)
                return Redirect("ErrorPage");

            return View();
        }
        public IActionResult Booking(Course course)
        {
            if (bookingViewModel == null)
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
                    RentDate = DateTime.Now.Date,
                };
            }
            try
            {
                bookingViewModel.CoursesForSelection = Data.ConvertCourseData.GetCourses().Courses;

            }
            catch (Exception e)
            {


            }
            finally
            {
                if (bookingViewModel == null)
                    bookingViewModel.CoursesForSelection = new List<Course>();
            }

            return Redirect("InfoPage");
        }


        public IActionResult ConfirmBooking(BookingViewModel booking)
        {
            foreach (var item in Data.ConvertCourseData.GetCourses().Courses)
            {
                if (item.Name == userBooking.CourseModel.Name)
                {
                    userBooking.HostRentedForCourse.TurnInDate = userBooking.RentDate.AddDays(item.Duration);
                    userBooking.CurrentUser = Data.Convertlogindata.AutoLogin();
                    userBooking.RentedClient = userBooking.CurrentUser.Initials;
                }
            }
            return View(userBooking);
        }

        public IActionResult BookingSucces()
        {
            //Send mail to user

            //Make a booking save file
            List<Item> hosts = Data.ConvertItemData.GetItems().Items;
            List<Course> courses = Data.ConvertCourseData.GetCourses().Courses;
            User user = new User();

            foreach (var item in hosts)
            {
                if (item.NumberOfPeoplePerHost >= userBooking.CourseModel.NumberOfStudents && item.Rented == false)
                {

                    //sets the host to rented
                    item.Rented = true;
                    //sets the hosts renteddate to the day it was rented
                    item.RentedDate = userBooking.RentDate;
                    //sets turnindate to day it was rented plus days its rented for aka turnindate
                    foreach (var course in courses)
                    {
                        if (userBooking.CourseModel.Name == course.Name)
                        {
                            item.TurnInDate = userBooking.RentDate.AddDays(course.Duration);
                            Data.ConvertItemData.EditItem(item);
                        }
                    }

                    userBooking.HostRentedForCourse = item;
                    //booking.RentedClient = convertlogindata.AutoLogin().Initials;
                    Data.ConvertBookingData.SaveBooking(userBooking);
                    break;
                }
            }
            foreach (var item in Data.ConvertUserData.GetUsers().Users)
            {
                if (userBooking.RentedClient == item.Initials)
                {
                    user.Email = item.Email;
                    return View(user);
                }
            }
            return View(new User());
        }
        #endregion
        #endregion

>>>>>>> Stashed changes

        public IActionResult Privacy()
        {
            var ItemModel = new ItemViewModel();
            ItemModel.Items = TestData.GetItems();
            return View(ItemModel);
        }

        [HttpGet]
        public IActionResult AdminSite()
        {
<<<<<<< Updated upstream
            ItemViewModel itemModel = ConvertData.GetItems();
            if (itemModel == null)
=======
            if (CurrentUser == null && CurrentUser.Admin == true)
                return Redirect("ErrorPage");

            ItemViewModel itemModel = Data.ConvertItemData.GetItems();
            try
>>>>>>> Stashed changes
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
            return View(Course);
        }

        [HttpPost]
        public IActionResult InfoPage(Course course)
        {
            return Redirect("/Home");
        }


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
            ConvertData.AddItem(item);
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
            ConvertData.EditItem(item);
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
            ConvertData.DeleteItem(SelectedItem);
            return Redirect("AdminSite");
        }
        #endregion
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

<<<<<<< Updated upstream
        //Need to find a way around this
        [HttpPost]
        public IActionResult UserPage(UserViewModel user, int id)
=======
        public IActionResult Bookings()
        {
            List<BookingViewModel> model = new List<BookingViewModel>();
            model = Data.ConvertBookingData.GetBookings();
            return View(model);
        }




        //Overview over all user pages
        #region User Pages
        [HttpGet]
        public IActionResult UserPage()
        {
            if (CurrentUser == null && CurrentUser.Admin == true)
                return Redirect("Home/ErrorPage");

            UserViewModel userModel = Data.ConvertUserData.GetUsers();
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
            Data.ConvertUserData.AddUser(user);
            return Redirect("/Home/UserPage");
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            if (CurrentUser == null && CurrentUser.Admin == true)
                return Redirect("Home/ErrorPage");

            //returns the AddUser page 
            return View();
        }

        [HttpGet]
        public IActionResult EditUser(UserViewModel userList, int id)
        {
            if (CurrentUser == null && CurrentUser.Admin == true)
                return Redirect("Home/ErrorPage");

            return View(SelectedUser);
        }
        [HttpPost]
        public IActionResult EditUser(User user)
        {
            Data.ConvertUserData.EditUser(user);
            return Redirect("UserPage");
        }


        [HttpGet]
        public IActionResult DeleteUser(UserViewModel user, int id)
        {
            if (CurrentUser == null && CurrentUser.Admin == true)
                return Redirect("Home/ErrorPage");

            //Sends the right user to the delete view
            SelectedUser = user.Users[id];
            return View(SelectedUser);
        }
        [HttpPost]
        public IActionResult DeleteUser(User user)
        {
            Data.ConvertUserData.DeleteUser(user);
            return Redirect("UserPage");
        }


        #endregion

        //Overview over all item pages
        #region Item Pages

        #region Add Item
        [HttpGet]
        public IActionResult AddItem()
        {
            if (CurrentUser == null && CurrentUser.Admin == true)
                return Redirect("Home/ErrorPage");

            return View();
        }

        [HttpPost]
        public IActionResult AddItem(Item item)
        {
            Data.ConvertItemData.AddItem(item);
            return Redirect("AdminSite");
        }
        #endregion

        #region Edit Item
        [HttpGet]
        public IActionResult EditItem(ItemViewModel item, int id)
        {
            if (CurrentUser == null && CurrentUser.Admin == true)
                return Redirect("Home/ErrorPage");

            return View(item.Items[id]);
        }

        [HttpPost]
        public IActionResult EditItem(Item item)
        {
            //Checks after a booking on the host and delete it aswell
            List<BookingViewModel> bookings = Data.ConvertBookingData.GetBookings();
            foreach (var booking in bookings)
            {
                if (item.Id == booking.Id)
                {
                    Data.ConvertBookingData.DeleteBooking(booking);
                }
            }
            Data.ConvertItemData.EditItem(item);
            return Redirect("AdminSite");
        }
        #endregion

        #region Delete Item 
        [HttpGet]
        public IActionResult DeleteItem(ItemViewModel item, int id)
        {
            if (CurrentUser == null && CurrentUser.Admin == true)
                return Redirect("Home/ErrorPage");

            SelectedItem = item.Items[id];
            return View(SelectedItem);
        }

        [HttpPost]
        public IActionResult DeleteItem(Item item)
        {
            Data.ConvertItemData.DeleteItem(item);
            //Checks after a booking on the host and delete it aswell
            List<BookingViewModel> bookings = Data.ConvertBookingData.GetBookings();
            foreach (var booking in bookings)
            {
                if (item.Id == booking.Id)
                {
                    Data.ConvertBookingData.DeleteBooking(booking);
                }
            }
            return Redirect("AdminSite");
        }
        #endregion
        #endregion

        //Overview over all course pages
        #region Course Pages

        public IActionResult CourseSite()
        {
            if (CurrentUser == null && CurrentUser.Admin == true)
                return Redirect("Home/ErrorPage");

            CourseViewModel viewModel = new CourseViewModel();
            viewModel = Data.ConvertCourseData.GetCourses();
            if (viewModel == null)
                return View(new CourseViewModel());
            else
                return View(viewModel);
        }


        #region Add Course
        [HttpGet]
        public IActionResult AddCourse()
        {
            if (CurrentUser == null && CurrentUser.Admin == true)
                return Redirect("Home/ErrorPage");

            return View();
        }

        [HttpPost]
        public IActionResult AddCourse(Course course)
        {
            Data.ConvertCourseData.AddCourse(course);
            return Redirect("CourseSite");
        }
        #endregion

        #region Edit Course
        [HttpGet]
        public IActionResult EditCourse(CourseViewModel course, int id)
        {
            if (CurrentUser == null && CurrentUser.Admin == true)
                return Redirect("Home/ErrorPage");

            return View(course.Courses[id]);
        }

        [HttpPost]
        public IActionResult EditCourse(Course course)
        {
            Data.ConvertCourseData.EditCourse(course);
            return Redirect("CourseSite");
        }
        #endregion

        #region Delete Course
        [HttpGet]
        public IActionResult DeleteCourse(CourseViewModel course, int id)
        {
            if (CurrentUser == null && CurrentUser.Admin == true)
                return Redirect("Home/ErrorPage");

            bookingViewModel.CourseModel = course.Courses[id];
            return View(course.Courses[id]);
        }

        [HttpPost]
        public IActionResult DeleteCourse(Course course)
        {
            Data.ConvertCourseData.DeleteCourse(course);
            return Redirect("CourseSite");
        }
        #endregion


        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
>>>>>>> Stashed changes
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
