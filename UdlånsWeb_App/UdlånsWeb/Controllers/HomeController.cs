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
        Data Data = new Data();
        private static User SelectedUser { get; set; }
        private static User CurrentUser { get; set; }
        private static Item SelectedItem { get; set; }
        private static BookingViewModel bookingViewModel { get; set; }
        private static BookingViewModel userBooking { get; set; }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        #region HomePage - Login page
        [HttpGet]
        public IActionResult HomePage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult HomePage(string initials)
        {
            //Add logic for login
            CurrentUser = Data.Convertlogindata.ManuelLogin(initials);

            if (CurrentUser == null)
                return Redirect("ErrorPage");

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

            int allocated = 0;

            foreach (var item in hosts)
            {
                if (item.Rented == false && booking.NumberOfGroups > allocated)
                {

                    //sets turnindate to day it was rented plus days its rented for aka turnindate
                    foreach (var course in courses)
                    {
                        if (booking.CourseModel.Name == course.Name)
                        {
                            booking.Id = item.Id;
                            booking.CourseModel.NumberOfGroupsPerHost = course.NumberOfGroupsPerHost;
                        }
                    }

                    allocated += booking.CourseModel.NumberOfGroupsPerHost;
                    booking.HostRentedForCourse.Add(item);
                    //booking.RentedClient = convertlogindata.AutoLogin().Initials;

                    //break;
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
        [HttpPost]
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
                    foreach (Item host in userBooking.HostRentedForCourse)
                    {
                        host.TurnInDate = userBooking.RentDate.AddDays(item.Duration);
                    }
                    //userBooking.HostRentedForCourse.TurnInDate = userBooking.RentDate.AddDays(item.Duration);
                    userBooking.CurrentUser = CurrentUser;
                    userBooking.RentedClient = userBooking.CurrentUser.Initials;
                }
            }
            return View(userBooking);
        }

        public IActionResult BookingSucces()
        {
            //Make a booking save file
            List<Item> hosts = Data.ConvertItemData.GetItems().Items;
            List<Course> courses = Data.ConvertCourseData.GetCourses().Courses;
            
            foreach (var item in userBooking.HostRentedForCourse)
            {
                if (item.Rented == false)
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
                }
            }
            Data.ConvertBookingData.SaveBooking(userBooking);

            // send mail to user and admins
            //MailSending.Email

            return View(userBooking);
        }
        #endregion
        #endregion

        public IActionResult Privacy()
        {
            var ItemModel = new ItemViewModel();
            ItemModel.Items = TestData.GetItems();
            return View(ItemModel);
        }

        [HttpGet]
        public IActionResult AdminSite()
        {
            if (CurrentUser == null || CurrentUser.Admin == false)
                return Redirect("ErrorPage");

            ItemViewModel itemModel = Data.ConvertItemData.GetItems();
            try
            {
                if (itemModel == null)
                {
                    itemModel = new ItemViewModel();
                }
                foreach (var item in itemModel.Items)
                {
                    //if host is rented 
                    if (item.Rented == true)
                    {
                        if (item.TurnInDate <= DateTime.Now)
                        {
                            item.Rented = false;
                        }

                        ////check hosts for a turn in date                         
                        //foreach (var host in itemModel.Items)
                        //{
                        //    if (host.TurnInDate == DateTime.Now.Date)
                        //    {
                        //        item.Rented = false;
                        //    }
                        //}
                    }

                }
            }
            catch (Exception)
            {
                return View(new ItemViewModel());
            }
            return View(itemModel);
        }

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
            if (CurrentUser == null || CurrentUser.Admin == false)
                return Redirect("ErrorPage");

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
            if (CurrentUser == null || CurrentUser.Admin == false)
                return Redirect("ErrorPage");

            //returns the AddUser page 
            return View();
        }

        [HttpGet]
        public IActionResult EditUser(UserViewModel userList, int id)
        {
            if (CurrentUser == null || CurrentUser.Admin == false)
                return Redirect("ErrorPage");

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
            if (CurrentUser == null || CurrentUser.Admin == false)
                return Redirect("ErrorPage");

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
            if (CurrentUser == null || CurrentUser.Admin == false)
                return Redirect("ErrorPage");

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
            if (CurrentUser == null || CurrentUser.Admin == false)
                return Redirect("ErrorPage");

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
            if (CurrentUser == null || CurrentUser.Admin == false)
                return Redirect("ErrorPage");

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
            if (CurrentUser == null || CurrentUser.Admin == false)
                return Redirect("ErrorPage");

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
            if (CurrentUser == null || CurrentUser.Admin == false)
                return Redirect("ErrorPage");

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
            if (CurrentUser == null || CurrentUser.Admin == false)
                return Redirect("ErrorPage");

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
            if (CurrentUser == null || CurrentUser.Admin == false)
                return Redirect("ErrorPage");

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
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ErrorPage()
        {
            return View(new ErrorViewModel());
        }
    }
}
