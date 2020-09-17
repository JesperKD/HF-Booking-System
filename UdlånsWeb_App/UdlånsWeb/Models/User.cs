using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace UdlånsWeb.Models
{
    public class User
    {
        [DisplayName("Fulde Navn")]
        public string Name { get; set; }
        [DisplayName("Initialer")]
        public string Initials { get; set; }
        public string Email { get; set; }
        [DisplayName("Admin Rettigheder")]
        public bool Admin { get; set; }
        public int Id { get; set; }
        [DisplayName("Adgangskode")]
        public string Password { get; set; }

    }
}
