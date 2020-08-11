using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace UdlånsWeb
{
    class ToTxt
    {
        /// <summary>
        /// Takes the string and write it to a txt file in the selected path
        /// </summary>
        /// <param name="path"></param>
        /// <param name="input"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Takes a string and add it to an existing file in a selected path
        /// </summary>
        /// <param name="path"></param>
        /// <param name="input"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Takes a string array(used by admin to make a changes to the list of users) and writes it to a selected file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="input"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Takes a string array(Used by admin to add new users) 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="input"></param>
        /// <returns></returns>
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
