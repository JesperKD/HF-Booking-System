using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace UdlånsWeb.Models
{
    public class User : IComparable<User>
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

        public int CompareTo(User other)
        {
            if (other == null)
                return 1;
            else
             return this.Id.CompareTo(other.Id);
        }
    }
}
