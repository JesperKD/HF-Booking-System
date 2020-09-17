using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdlånsWeb.Models
{
    public class Login
    {
        public string Initials { get; set; }
        public string Password { get; set; }
        public bool Valid { get; set; }
    }
}
