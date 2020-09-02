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

namespace UdlånsWeb.DataHandling
{

    public class ConvertLoginData
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;


        ConvertUserData convertuserdata = new ConvertUserData();
        public bool loginConfirmed = false;

        public User AutoLogin()
        {
            User user = new User();

            string initials = Environment.UserName;
            string[] iniSplit = initials.Split('.');

            UserViewModel userModel = convertuserdata.GetUsers();

            try
            {
                if (userModel.Users.Any(x => x.Initials == iniSplit[0].ToUpper() && x.Password == user.Password))
                {
                    user = userModel.Users.Where(x => x.Initials == iniSplit[0].ToUpper() && x.Password == user.Password).FirstOrDefault();
                    return user;
                }
            }
            catch (Exception)
            {
                return null;
            }
            return null;

        }

        public User ManuelLogin(string Initials)
        {
            User user = new User();

            UserViewModel userModel = convertuserdata.GetUsers();
            try
            {
                if (userModel.Users.Any(x => x.Initials == Initials.ToUpper() && x.Password == user.Password))
                {
                    user = userModel.Users.Where(x => x.Initials.ToUpper() == user.Initials.ToUpper() && x.Password == user.Password).FirstOrDefault();
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
