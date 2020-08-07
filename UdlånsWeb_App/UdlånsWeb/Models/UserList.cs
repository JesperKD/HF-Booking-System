using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdlånsWeb.DataHandling;

namespace UdlånsWeb.Models
{
    public class UserList
    {
        public List<User> Users { get; set; }
        public UserList()
        {
            Users = new List<User>();
        }

    }
}
