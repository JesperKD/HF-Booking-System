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
    public class BookingController : Controller
    {
        private static BookingViewModel bookingViewModel { get; set; }
        private static BookingViewModel userBooking { get; set; }
        
        [HttpGet]
        public IActionResult BookingDefine()
        {
            if (CurrentUser.User == null)
                return Redirect("Home/ErrorPage");
            
            

            return View();
        }
        [HttpGet]
        public IActionResult InfoPage(Course course)
        {
            if (CurrentUser.User == null)
                return Redirect("Home/ErrorPage");

            if (bookingViewModel == null)
            {
                bookingViewModel = new BookingViewModel
                {
                    CourseModel = new Course()
                    {
                        Name = course.Name,
                        CourseNumber = course.CourseNumber,
                        Defined = course.Defined
                    },

                    RentDate = DateTime.Now.Date,
                };
            }
            
            bookingViewModel.CoursesForSelection = Data.GetCourses().Courses;
            bookingViewModel.CurrentUser = CurrentUser.User;
            return View(bookingViewModel);
        }

        [HttpPost]
        public IActionResult InfoPage(BookingViewModel booking)
        {
            //Finds the course matching the chosen course name
            Course course = Data.GetCourses().Courses.Where(x => x.Id == booking.CourseModel.Id).FirstOrDefault();
            //Sets the course defined to user choise
            course.Defined = booking.CourseModel.Defined;
            //Sets the number of students for the selected course
            course.NumberOfStudents = booking.CourseModel.NumberOfStudents;
            //Puts the user defined data to the booking view model
            booking.CourseModel = course;
            //Adds the booking to userBooking for save later
            userBooking = booking;
            return Redirect("ConfirmBooking");
        }

        public IActionResult ConfirmBooking(BookingViewModel booking)
        {
            int studentsRemaining = userBooking.CourseModel.NumberOfStudents;
            var hostViewModel = Data.GetHosts();
            foreach (var item in hostViewModel.Hosts)
            {
                //Makes sure that there is enough hosts for the class
                if (item.NumberOfPeoplePerHost <= studentsRemaining)
                {
                    if(userBooking.CourseModel.Defined == false)
                    {
                        userBooking.TurnInDate = userBooking.RentDate.AddDays(userBooking.CourseModel.Duration);
                    }
                    studentsRemaining -= item.NumberOfPeoplePerHost;
                    userBooking.CurrentUser = CurrentUser.User;
                }
            }

            Data.BookingData.Add(userBooking);
            
            return View(userBooking);
        }

        public IActionResult BookingSucces()
        {
            
            Data.SaveBookings();
            return View(CurrentUser.User);    
        }

        public IActionResult Bookings()
        {
            if(Data.GetBookings().Count != 0 || Data.GetBookings() != null)
            {
                Data.GetBookings();
                return View(Data.BookingData);
            }
            else
            {
                return View(new BookingViewModel());
            }
        }

    }
}
