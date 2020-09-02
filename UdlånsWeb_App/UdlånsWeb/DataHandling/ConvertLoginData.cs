using Microsoft.AspNetCore.Mvc.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdlånsWeb.Models;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Razor.Language;
using System.Security.Cryptography.X509Certificates;

namespace UdlånsWeb.DataHandling
{

    public class ConvertLoginData
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;


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
            string[] iniSplit = initials.Split('.');

            UserViewModel userModel = convertuserdata.GetUsers();

            try
            {
                if (userModel.Users.Any(x => x.Initials == iniSplit[0].ToUpper()))
                {
                    user = userModel.Users.Where(x => x.Initials == iniSplit[0].ToUpper()).FirstOrDefault();
                    return user;
                }
            }
            catch (Exception)
            {
                return null;
            }
            return null;

        }

        public User ManuelLogin(string Initials, string pass)
        {
            User user = new User();

            UserViewModel userModel = convertuserdata.GetUsers();
            try
            {
                if (userModel.Users.Any(x => x.Initials == Initials.ToUpper() && x.Password == pass))
                {
                    user = userModel.Users.Where(x => x.Initials == Initials.ToUpper() && x.Password == pass).FirstOrDefault();
                    return user;
                }
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }
    }
}
