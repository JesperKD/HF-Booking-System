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
            userBooking = booking;

            
            return Redirect("ConfirmBooking");
        }

        [HttpGet]
        public IActionResult BookingDefine()
        {
            if (CurrentUser.User == null)
                return Redirect("Home/ErrorPage");
            
            

            return View();
        }


        public IActionResult ConfirmBooking(BookingViewModel booking)
        {
            int studentsRemaining = booking.CourseModel.NumberOfStudents;
            
            foreach (var item in Data.HostViewModel.Hosts)
            {
                if (item.NumberOfPeoplePerHost >= studentsRemaining)
                {
                    studentsRemaining -= item.NumberOfPeoplePerHost;
                    userBooking.CurrentUser = CurrentUser.User;

                }
            }

            if(booking.CourseModel.Defined == false)
            booking.TurnInDate = booking.RentDate.AddDays(booking.CourseModel.Duration);

            Data.BookingViewModels.Add(booking);
            
            return View(userBooking);
        }

        public IActionResult BookingSucces()
        {
            
            Data.SaveBookings();
            return View(CurrentUser.User);    
        }

        public IActionResult Bookings()
        {
            return View(Data.GetBookings());
        }

    }
}
