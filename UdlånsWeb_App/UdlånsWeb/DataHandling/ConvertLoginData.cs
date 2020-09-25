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
