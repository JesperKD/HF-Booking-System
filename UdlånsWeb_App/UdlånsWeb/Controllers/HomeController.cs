﻿using System;
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
        private static PasswordGenerator passwordGenerator = new PasswordGenerator();
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
        public IActionResult HomePage(Login login)
        {
            //Add logic for login
            CurrentUser = Data.Convertlogindata.ManuelLogin(login.Initials, login.Password);

            if (CurrentUser == null)
                return Redirect("ErrorPage");

            if (CurrentUser.Admin == true)
                return Redirect("/Home/AdminSite");

            else
                return Redirect("Home/Booking");
        }
        #endregion

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
            List<BookingViewModel> bookings = Data.ConvertBookingData.GetBookings();

            if (bookings == null)
            {
                booking.Id = 0;
            }
            else
            {
                booking.Id = bookings.Count;
            }

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
                            // fjern den her
                            //booking.Id = item.Id;
                            booking.CourseModel.NumberOfGroupsPerHost = course.NumberOfGroupsPerHost;
                        }
                    }

                    allocated += booking.CourseModel.NumberOfGroupsPerHost;
                    booking.HostRentedForCourse.Add(item);
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
                    userBooking.CurrentUser = CurrentUser;
                    userBooking.RentedClient = userBooking.CurrentUser.Initials;
                }
            }
            return View(userBooking);
        }

        // need a get and post
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
            UserViewModel users = Data.ConvertUserData.GetUsers();
            UserViewModel mailRecipients = new UserViewModel();
            foreach (User user in users.Users)
            {
                if (user.Email == CurrentUser.Email && user.Initials == CurrentUser.Initials)
                {
                    mailRecipients.Users.Add(user);
                    continue;
                }
                if (user.Admin == true)
                {
                    mailRecipients.Users.Add(user);
                    continue;
                }
            }
            // New stringbuilder
            StringBuilder stringBuilder = new StringBuilder();

            // String for email
            stringBuilder.Append("Booking summary:" + Environment.NewLine + "Lærer initialer: " + CurrentUser.Initials
                + Environment.NewLine + "Fag: " + userBooking.CourseModel.Name + Environment.NewLine
                + "Booket fra " + userBooking.HostRentedForCourse.First().RentedDate + " - til " + userBooking.HostRentedForCourse.First().TurnInDate
                + Environment.NewLine + "Hostnavn       Ip      User        Password" + Environment.NewLine
                );

            foreach (var item in userBooking.HostRentedForCourse)
            {
                stringBuilder.Append(item.HostName + "          " + item.HostIp + "         " + item.UserName + "            " + item.HostPassword + Environment.NewLine);
            }

            stringBuilder.Append("Antal grupper pr host [" + userBooking.CourseModel.NumberOfGroupsPerHost + "]" + Environment.NewLine
                + Environment.NewLine + "Adgang til serveren kan etableres via følgnde netværk:"
                + "Trådløst (Når eleverne er på skolen) - Forbind til DataExpNet" + Environment.NewLine
                + "(kode: Just@Salt&Vinegar666)" + Environment.NewLine
                + "VPN (Når eleverne er hjemme) - Følg vejledningen til installation" + Environment.NewLine
                + "af VPN forbindelsen og forbind hrefter med jeres ZBC initialer" + Environment.NewLine
                + "(Kode: Just@Salt&Vinegar666)" + Environment.NewLine + Environment.NewLine
                + "!!! Husk at bede dine elever om at ryde op på hostn inden" + Environment.NewLine
                + "faget slutter !!!");

            MailSending.Email(stringBuilder.ToString(), mailRecipients);

            return View(userBooking);
        }
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
                        if (item.TurnInDate < DateTime.Now)
                        {
                            item.Rented = false;
                            Data.ConvertItemData.EditItem(item);
                        }
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
        public IActionResult UserPage(int id)
        {
            UserViewModel users = Data.ConvertUserData.GetUsers();
            SelectedUser = users.Users[id];
            SelectedUser.Id = id;

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
        public IActionResult EditUser()
        {
            if (CurrentUser == null || CurrentUser.Admin == false)
                return Redirect("ErrorPage");

            return View(SelectedUser);
        }
        [HttpPost]
        public IActionResult EditUser(User user)
        {
            user.Id = SelectedUser.Id;
            Data.ConvertUserData.EditUser(user);
            return Redirect("UserPage");
        }


        [HttpGet]
        public IActionResult DeleteUser(int id)
        {
            if (CurrentUser == null || CurrentUser.Admin == false)
                return Redirect("ErrorPage");

            //Sends the right user to the delete view
            UserViewModel users = Data.ConvertUserData.GetUsers();
            SelectedUser = users.Users[id];


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
        public IActionResult EditItem(int id)
        {
            if (CurrentUser == null || CurrentUser.Admin == false)
                return Redirect("ErrorPage");

            ItemViewModel items = Data.ConvertItemData.GetItems();

            return View(items.Items[id]);
        }

        [HttpPost]
        public IActionResult EditItem(Item item)
        {
            //Checks after a booking on the host and delete it aswell
            List<BookingViewModel> bookings = Data.ConvertBookingData.GetBookings();
            var items = Data.ConvertItemData.GetItems();
            foreach (var booking in bookings)
            {
                foreach (var host in booking.HostRentedForCourse)
                {
                    if (host.HostName == item.HostName)
                    {
                        foreach (var hosts in booking.HostRentedForCourse)
                        {
                            Item edit = items.Items.Where(x => x.HostName == hosts.HostName && x.Id == hosts.Id).FirstOrDefault();
                            edit.Rented = false;
                            // doesnt get the entire host..
                            Data.ConvertItemData.EditItem(edit);
                        }
                        Data.ConvertBookingData.DeleteBooking(booking);
                        break;
                    }
                }
            }
            if (item.TurnInDate == DateTime.Parse("01-01-0001 00:00:00") && item.Rented == true)
            {
                item.TurnInDate = DateTime.Now.AddYears(1);
            }
            else
            {
                Data.ConvertItemData.EditItem(item);
            }
            return Redirect("AdminSite");
        }
        #endregion

        #region Delete Item 
        [HttpGet]
        public IActionResult DeleteItem(int id)
        {
            if (CurrentUser == null || CurrentUser.Admin == false)
                return Redirect("ErrorPage");

            ItemViewModel items = Data.ConvertItemData.GetItems();

            Item item = items.Items[id];

            return View(item);
        }

        [HttpPost]
        public IActionResult DeleteItem(Item item)
        {
            Data.ConvertItemData.DeleteItem(item);
            //Checks after a booking on the host and delete it aswell
            List<BookingViewModel> bookings = Data.ConvertBookingData.GetBookings();
            foreach (var booking in bookings)
            {
                foreach (var host in booking.HostRentedForCourse)
                {
                    if (host.HostName == item.HostName)
                    {
                        Data.ConvertBookingData.DeleteBooking(booking);
                        break;
                    }
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
        public IActionResult EditCourse(int id)
        {
            if (CurrentUser == null || CurrentUser.Admin == false)
                return Redirect("ErrorPage");

            CourseViewModel course = Data.ConvertCourseData.GetCourses();

            return View(course.Courses[id]);
        }

        [HttpPost]
        public IActionResult EditCourse(Course changed, int id)
        {
            CourseViewModel course = Data.ConvertCourseData.GetCourses();


            Data.ConvertCourseData.EditCourse(changed, id);
            return Redirect("CourseSite");
        }
        #endregion

        #region Delete Course
        [HttpGet]
        public IActionResult DeleteCourse(int id)
        {
            if (CurrentUser == null || CurrentUser.Admin == false)
                return Redirect("ErrorPage");

            CourseViewModel course = Data.ConvertCourseData.GetCourses();

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

        // Password related methods
        #region Password
        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ResetPassword(string email)
        {
            string newPass = passwordGenerator.Generate();

            User user = new User();
            UserViewModel users = Data.ConvertUserData.GetUsers();
            user = users.Users.Where(x => x.Email == email).FirstOrDefault();
            user.Password = newPass;

            Data.ConvertUserData.EditUser(user);
            // send mail here with new pass
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Nyt password" + Environment.NewLine + Environment.NewLine + "Dit nye password er: " + user.Password);

            UserViewModel recipient = new UserViewModel();
            recipient.Users.Add(user);

            MailSending.Email(stringBuilder.ToString(), recipient);

            return Redirect("HomePage");
        }
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ChangePassword(ChangePassword change)
        {
            // save user
            User user = new User();
            UserViewModel users = Data.ConvertUserData.GetUsers();

            user = users.Users.Where(x => x.Initials == change.UserName.ToUpper() && x.Email == change.Email).FirstOrDefault();
            if (user.Initials != null && user.Email != null && user.Password == change.OldPassword)
            {
                user.Password = change.NewPassword;
                Data.ConvertUserData.EditUser(user);
            }
            else
            {
                return Redirect("ErrorPage");
            }

            // send mail
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Ændring af password" + Environment.NewLine + Environment.NewLine + "Dit password er nu blevet ændret til: " + user.Password);

            UserViewModel recipient = new UserViewModel();
            recipient.Users.Add(user);

            MailSending.Email(stringBuilder.ToString(), recipient);

            return Redirect("HomePage");
        }
        public IActionResult PasswordConfirmation()
        {
            return View();
        }
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
