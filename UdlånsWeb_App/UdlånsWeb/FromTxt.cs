using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace UdlånsWeb
{
    class FromTxt
    {
        OpenFileDialog fileDialog = new OpenFileDialog();

        public string StringFromTxt()
        {
            // lets user select the file to read
            fileDialog.ShowDialog();

            if (fileDialog.FileName != null && fileDialog.FileName != string.Empty)
            {
                // reads file into result string
                string result = File.ReadAllText(fileDialog.FileName);

                // resets the file dialog
                fileDialog.FileName = string.Empty;

                // returns result string
                return result;
            }
            else
            {
                return null;
            }
        }

        public string[] StringsFromTxt()
        {
            // lets user select the file to read
            fileDialog.ShowDialog();

            if (fileDialog.FileName != null && fileDialog.FileName != string.Empty)
            {
                // reads file into result array
                string[] result = File.ReadAllLines(fileDialog.FileName);

                // resets the filedialog
                fileDialog.FileName = string.Empty;

                // returns result array
                return result;
            }
            else
            {
                return null;
            }
        }

        public char[] CharsFromTxt()
        {
            // lets user select the file to read
            fileDialog.ShowDialog();

            if (fileDialog.FileName != null && fileDialog.FileName != string.Empty)
            {
                // reads file into string text
                string text = File.ReadAllText(fileDialog.FileName);

                // converts string text to result char array
                char[] result = text.ToCharArray();

                // resets the filedialog
                fileDialog.FileName = string.Empty;

                // returns result char array
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
