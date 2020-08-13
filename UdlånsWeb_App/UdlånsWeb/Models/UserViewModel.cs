using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdlånsWeb.Models
{
    public class UserViewModel
    {
        public List<User> Users { get; set; }
        public int Id { get; set; }
        public UserViewModel()
        {
            Users = new List<User>();
        }
    }
}
