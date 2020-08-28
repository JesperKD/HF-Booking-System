using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace UdlånsWeb.Models
{
    public class Course
    {
        [DisplayName("Fag")]
        public string Name { get; set; }
        [DisplayName("Fag nummer")]
        public int CourseNumber { get; set; }
        [DisplayName("Antal grupper pr. host")]
        public int NumberOfStudents { get; set; }
        [DisplayName("Standart varighed")]
        public int Duration { get; set; }
        public bool Defined { get; set; }
        public int Id { get; set; }
    }
}
