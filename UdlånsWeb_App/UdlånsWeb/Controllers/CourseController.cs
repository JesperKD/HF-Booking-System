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
        public IActionResult CourseSite()
        {
            if (CurrentUser.User == null || CurrentUser.User.Admin == false)
                return Redirect("Home/ErrorPage");

            Data.CourseData = Data.GetCourses();

            if (Data.CourseData.Courses.Count != 0)
                return View(Data.CourseData);
            else
                return View(new CourseViewModel());
        }


        [HttpGet]
        public IActionResult AddCourse()
        {
            if (CurrentUser.User == null || CurrentUser.User.Admin == false)
                return Redirect("Home/ErrorPage");

            return View();
        }

        [HttpPost]
        public IActionResult AddCourse(Course course)
        {

            Data.CourseData.Courses.Add(course);
            Data.SaveCourses();
            return Redirect("CourseSite");
        }
        
        [HttpGet]
        public IActionResult EditCourse(CourseViewModel course)
        {
            if (CurrentUser.User == null || CurrentUser.User.Admin == false)
                return Redirect("Home/ErrorPage");
            
            return View(course.Courses[course.Id]);
        }

        [HttpPost]
        public IActionResult EditCourse(Course course)
        {
            Data.EditCourse(course);
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
            Data.DeleteCourse(course);
            return Redirect("CourseSite");
        }
    }
}
