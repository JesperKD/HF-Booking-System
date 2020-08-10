using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace UdlånsWeb
{
    class ToTxt
    {
        public bool StringToTxt(string path, string input)
        {
            try
            {
                File.WriteAllText(path, input);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool AppendStringToTxt(string path, string input)
        {
            try
            {
                File.AppendAllText(path, input);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool StringsToTxt(string path, string[] input)
        {
            try
            {
                File.WriteAllLines(path, input);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool AppendStringsToTxt(string path, string[] input)
        {
            try
            {
                File.AppendAllLines(path, input);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool CharToTxt(string path, char input)
        {
            try
            {
                File.WriteAllText(path, input.ToString());
                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool AppendCharToTxt(string path, char input)
        {
            try
            {
                File.AppendAllText(path, input.ToString());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CharsToTxt(string path, char[] input)
        {
            try
            {
                File.WriteAllText(path, input.ToString());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AppendCharsToTxt(string path, char[] input)
        {
            try
            {
                File.AppendAllText(path, input.ToString());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
