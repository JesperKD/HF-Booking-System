using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace UdlånsWeb.Models
{
    public class Course : IComparable<Course>
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
        public int CompareTo(Course other)
        {
            if (other == null)
                return 1;
            else
                return this.Id.CompareTo(other.Id);
        }

      
    }
}
