using Microsoft.AspNetCore.Mvc.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdlånsWeb.Models;

namespace UdlånsWeb.DataHandling
{
    public class ConvertLoginData
    {
        ConvertUserData convertuserdata = new ConvertUserData();
        public bool loginConfirmed = false;

        public void CheckLogin(String input)
        {
            try
            {
                foreach (User user in convertuserdata.GetUsers().Users)
                {
                    if (user.Initials == input)
                    {
                        Console.WriteLine("Access granted");
                        loginConfirmed = true;
                    }
                    else
                    {
                        Console.WriteLine("Access denied");
                        loginConfirmed = false;
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Login method failed");
            }
        }

        public User AutoLogin()
        {
            User user = new User();
            string initials = Environment.UserName;

            UserViewModel userModel = convertuserdata.GetUsers();

            if (userModel.Users.Any(x => x.Initials == initials.ToUpper()))
            {
                user = userModel.Users.Where(x => x.Initials == initials).FirstOrDefault();
            }

            return user;
        }
    }
}
