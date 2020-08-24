using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace UdlånsWeb.Models
{
    public class Course
    {
        [DisplayName("Fag")]
        public string Name { get; set; }
        [DisplayName("Start Dato")]
        public DateTime StartDate { get; set; }
        [DisplayName("Slut Dato")]
        public DateTime EndDate { get; set; }
        [DisplayName("Antal elever")]
        public int NumberOfStudents { get; set; }
        [DisplayName("Fagets varighed")]
        public int Duration { get; set; }
        public bool Difined { get; set; }

        public Course()
        {
            var time = DateTime.UtcNow;
            var timenow = new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, 0);
            StartDate = timenow;
            EndDate = timenow;
        }
    }
}
