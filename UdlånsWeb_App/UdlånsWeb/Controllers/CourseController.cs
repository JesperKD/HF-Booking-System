using Microsoft.AspNetCore.Mvc;
using System.Linq;
using UdlånsWeb.DataHandling;
using UdlånsWeb.DataHandling.DataCheckUp;
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
            Data.CourseData.Courses.Sort();
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
            IdControl.GiveIdToCourse(course);
            Data.GetCourses();
            Data.CourseData.Courses.Add(course);
            Data.SaveCourses();
            return Redirect("CourseSite");
        }
        
        [HttpGet]
        public IActionResult EditCourse(int id)
        {
            if (CurrentUser.User == null || CurrentUser.User.Admin == false)
                return Redirect("Home/ErrorPage");

            Data.GetCourses();
            Course courseForEdit = Data.CourseData.Courses.Where(x => x.Id == id).FirstOrDefault();
            return View(courseForEdit);
        }

        [HttpPost]
        public IActionResult EditCourse(Course course)
        {
            Data.EditCourse(course);
            return Redirect("CourseSite");
        }

        [HttpGet]
        public IActionResult DeleteCourse(int id)
        {
            if (CurrentUser.User == null || CurrentUser.User.Admin == false)
                return Redirect("Home/ErrorPage");
            Data.GetCourses();
            Course courseForDelete = Data.CourseData.Courses.Where(x => x.Id == id).FirstOrDefault();

            return View(courseForDelete);
        }

        [HttpPost]
        public IActionResult DeleteCourse(Course course)
        {
            Data.DeleteCourse(course);
            return Redirect("CourseSite");
        }
    }
}
