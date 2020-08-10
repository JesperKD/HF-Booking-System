using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace UdlånsWeb
{
    class FromTxt
    {
        public string[] StringsFromTxt(string path)
        {
            try
            {
                string[] result = File.ReadAllLines(path);
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
