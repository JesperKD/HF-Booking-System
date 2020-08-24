﻿using Microsoft.AspNetCore.Cors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UdlånsWeb.Models
{
    public class BookingViewModel
    {
        public Item HostRentedForCourse { get; set; } = new Item();
        public Course CourseModel { get; set; } = new Course();
        [DisplayName("Alle fag")]
        public List<Course> CoursesForSelection { get; set; } = new List<Course>();
    }
}
