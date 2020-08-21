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
        public DateTime EndData { get; set; }
        [DisplayName("Antal elever")]
        public int NumberOfStudents { get; set; }
        public bool Difined { get; set; }
    }
}
