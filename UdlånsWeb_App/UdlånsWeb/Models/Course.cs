using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace UdlånsWeb.Models
{
    public class Course
    {
        [DisplayName("Fag")]
        public string Name { get; set; }
        public string DisplayDate { get; set; }
        [DisplayName("Start Dato")]
        public DateTime StartDate { get; set; }
        [DisplayName("Slut Dato")]
        public DateTime EndDate { get; set; }
        [DisplayName("Antal elever")]
        public int NumberOfStudents { get; set; }
        [DisplayName("Fagets varighed")]
        public int Duration { get; set; }
        public bool Defined { get; set; }
        public int Id { get; set; }
        public Course()
        {
            //var time = DateTime.Now;
            //var timenow = new DateTime(time .Year, time.Day, time.Month, time.Hour, time.Minute, 0);

            DisplayDate = DateTime.Now.ToString("MM DD YY");
            StartDate = new DateTime();
            EndDate = new DateTime();
        }
    }
}
