using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdlånsWeb.Models;

namespace UdlånsWeb.DataHandling
{
    public static class ConvertData
    {
        public static List<User> ConverDataToUser(string[] data)
        {
            List<User> users = new List<User>();

            for (int i = 0; i < data.Length; i++)
            {
                User user = new User();

                user.Name = data[i];
                i++;
                user.Initials = data[i];
                i++;
                user.Email = data[i];
                i++;
                user.Admin = Convert.ToBoolean(data[i]);
                users.Add(user);
            }
            return users;
        }
    }
}
