using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdlånsWeb.DataHandling
{
    public class PasswordGenerator
    {
        public string Generate()
        {
            string result = string.Empty;
            Random random = new Random();
            for (int i = 0; i < 6; i++)
            {
                result += random.Next(0, 10);
            }

            return result;
        }
    }
}
