using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using UdlånsWeb.Models;

namespace UdlånsWeb.DataHandling
{
    public static class TestData
    {
        public static List<ItemModel> GetItems()
        {
            List<ItemModel> items = new List<ItemModel>() {
                new ItemModel(){HostName = "Host1", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new ItemModel(){HostName = "Host2", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.10.0", HostIp = "110.0.0.1", Rented = false }
            };
            return items;
        }

        public static List<User> GetUsers()
        {
            List<User> users = new List<User>() { 
                new User(){Name = "Flemming", Initials = "FlemAK", Email = "Flemse@ZBC.dk", Admin = true }, 
                new User(){Name = "Janni", Initials = "JanniS", Email = "JanniH@ZBC.dk", Admin = true}, 
            };

            return users;
        } 
    }
}
