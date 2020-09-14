using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdlånsWeb.Models;

namespace UdlånsWeb.DataHandling
{
    public static class PasswordGenerator
    {
        public static User Generate(User user)
        {
            string result = string.Empty;
            Random random = new Random();
            for (int i = 0; i < 6; i++)
            {
                result += random.Next(0, 10);
            }
            user.Password = result;
            return user;
        }
    }
}