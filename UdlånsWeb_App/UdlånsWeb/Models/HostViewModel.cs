using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace UdlånsWeb.Models
{
    public class HostViewModel
    {
        public int Id { get; set; }
        public List<Host> Hosts { get; set; }
        public List<BookingViewModel> Bookings { get; set; }
        public HostViewModel()
        {
            Hosts = new List<Host>();
            Bookings = new List<BookingViewModel>();
        }

    }
}
