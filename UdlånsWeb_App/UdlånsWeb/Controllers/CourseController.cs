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
        Data Data = new Data();
        private static User SelectedUser { get; set; }
        private static User CurrentUser { get; set; }
        private static Item SelectedItem { get; set; }
        private static BookingViewModel bookingViewModel { get; set; }
        private static BookingViewModel userBooking { get; set; }

        public IActionResult Index()
        {
            return View();
        }

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
    }
}
