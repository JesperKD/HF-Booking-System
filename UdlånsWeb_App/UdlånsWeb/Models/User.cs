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
        [DisplayName("Fornavn")]
        public string FirstName { get; set; }
        [DisplayName("Efternavn")]
        public string LastName { get; set; }
        [DisplayName("Initialer")]
        public string Initials { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool Admin { get; set; }
        public int Id { get; set; }

    }
}
