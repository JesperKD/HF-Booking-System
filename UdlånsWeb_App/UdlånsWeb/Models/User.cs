using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace UdlånsWeb.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Initials { get; set; }
        public string Email { get; set; }
        public bool Admin { get; set; }
        
    }
}
