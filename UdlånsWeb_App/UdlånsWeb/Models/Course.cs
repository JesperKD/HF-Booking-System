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
        public int NumberOfStudents { get; set; }
        [DisplayName("Fagets varighed")]
        public int Duration { get; set; }
        public bool Defined { get; set; }
        public int Id { get; set; }
    }
}
