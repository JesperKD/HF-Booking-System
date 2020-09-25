using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdlånsWeb.Models
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        public List<Course> Courses { get; set; }
        public CourseViewModel()
        {
            Courses = new List<Course>();
        }
    }
}
