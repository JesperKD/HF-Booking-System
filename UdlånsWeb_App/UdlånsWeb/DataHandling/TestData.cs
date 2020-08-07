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
                new ItemModel("Host1", "kode1234!", "root", "2.14.1", "101.0.0.1"),
                new ItemModel("Host2", "kode1234!", "root", "2.10.0", "110.0.0.1")
            };
            return items;
        }

        public static List<User> GetUsers()
        {
            List<User> users = new List<User>() { 
                new User(){Name = "Flemming", UserName = "FlemAK", Email = "Flemse@ZBC.dk",Password = "***********", Admin = false }, 
                new User(){Name = "Janni", UserName = "JanniS", Email = "JanniH@ZBC.dk", Password = "******", Admin = false}, 
            };

            return users;
        } 
    }
}
