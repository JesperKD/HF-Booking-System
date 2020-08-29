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
    public class CourseController : Controller
    {
        private static User SelectedUser { get; set; }
        private static Host SelectedItem { get; set; }
        private static BookingViewModel bookingViewModel { get; set; }
        private static BookingViewModel userBooking { get; set; }

        public IActionResult CourseSite()
        {
            if (CurrentUser.User == null && CurrentUser.User.Admin == false)
                return Redirect("Home/ErrorPage");

            
            if (Data.CourseViewModel == null)
                return View(new CourseViewModel());
            else
                return View(Data.CourseViewModel);
        }


        [HttpGet]
        public IActionResult AddCourse()
        {
            if (CurrentUser.User == null && CurrentUser.User.Admin == true)
                return Redirect("Home/ErrorPage");

            return View();
        }

        [HttpPost]
        public IActionResult AddCourse(Course course)
        {
            Data.CourseViewModel.Courses.Add(course);
            return Redirect("CourseSite");
        }
        
        [HttpGet]
        public IActionResult EditCourse(CourseViewModel course)
        {
            if (CurrentUser.User == null && CurrentUser.User.Admin == true)
                return Redirect("Home/ErrorPage");

            return View(course.Courses[course.Id]);
        }

        [HttpPost]
        public IActionResult EditCourse(Course course)
        {
            Data.EditCourses(course);
            return Redirect("CourseSite");
        }

        [HttpGet]
        public IActionResult DeleteCourse(CourseViewModel courseView)
        {
            if (CurrentUser.User == null || CurrentUser.User.Admin == false)
                return Redirect("Home/ErrorPage");

            return View(courseView.Courses[courseView.Id]);
        }

        [HttpPost]
        public IActionResult DeleteCourse(Course course)
        {
            Data.ConvertCourseData.DeleteCourse(course);
            return Redirect("CourseSite");
        }
    }
}
