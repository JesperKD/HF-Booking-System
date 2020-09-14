using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
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
                        Defined = course.Defined
                    },

                    RentDate = DateTime.Now.Date,
                };
            }
            
            bookingViewModel.CoursesForSelection = Data.GetCourses().Courses;
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
            userBooking.CurrentUser = CurrentUser.User;
            return Redirect("ConfirmBooking");
        }

        public IActionResult ConfirmBooking()
        {
            int availableSpotsForStudents = userBooking.CourseModel.NumberOfStudents;
            var hostViewModel = Data.GetHosts();
            foreach (var item in hostViewModel.Hosts)
            {
                //Makes sure that there is enough hosts for the class
                if (item.NumberOfPeoplePerHost >= availableSpotsForStudents && item.Rented == false)
                {
                    if(userBooking.CourseModel.Defined == false && userBooking.CourseModel.NumberOfStudents >= availableSpotsForStudents)
                    {
                        item.Rented = true;
                        userBooking.Id = item.Id;
                        userBooking.TurnInDate = userBooking.RentDate.AddDays(userBooking.CourseModel.Duration);
                        userBooking.HostsRentedForCourse.Add(item);
                        availableSpotsForStudents += item.NumberOfPeoplePerHost - availableSpotsForStudents;
                    }
                }
            }
            userBooking.CurrentUser = CurrentUser.User;
            Data.HostData.Bookings.Add(userBooking);
            return View(userBooking);
        }

        public IActionResult BookingSucces()
        {
            Data.SaveHosts();
            return View(userBooking);    
        }

        public IActionResult Bookings()
        {
            Data.HostData = Data.GetHosts();
            if(Data.HostData.Bookings != null || Data.HostData.Bookings.Count != 0)
            {
                return View(Data.HostData);
            }
            else
            {
                return View(new HostViewModel());
            }

        }
    }
}
