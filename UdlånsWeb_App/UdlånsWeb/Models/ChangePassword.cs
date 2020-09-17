using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdlånsWeb.Models
{
    public class ChangePassword
    {
        public string Initials { get; set; }
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public bool Valid { get; set; } = true;
    }
}
