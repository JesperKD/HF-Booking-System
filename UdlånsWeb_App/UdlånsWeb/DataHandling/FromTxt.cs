using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace UdlånsWeb
{
    class FromTxt
    {
        /// <summary>
        /// Used by app to read the txt file of users
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string[] StringsFromTxt(string path)
        {
            string[] result = new string[1];
            try
            {
                result = File.ReadAllLines(path);
                return result;
            }
            catch (Exception e)
            {
                result[0] = null;
            }
            return result;
        }
    }
}
