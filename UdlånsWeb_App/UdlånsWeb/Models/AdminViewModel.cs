using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdlånsWeb.Models
{
    public static class AdminViewModel
    {
        public static List<Host> Hosts { get; set; }
        public static List<BookingViewModel> Bookings { get; set; }
        public static List<Course> Courses { get; set; }
        public static List<User> Users { get; set; }
    }
}
