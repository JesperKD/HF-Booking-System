using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdlånsWeb.Models;

namespace UdlånsWeb.DataHandling
{
    public class ConvertUserData
    {
        ToTxt ToTxt = new ToTxt();
        FromTxt FromTxt = new FromTxt();
        Encrypt Encrypt;
        Decrypt Decrypt;
        const string FILE_NAME = "\\user.txt";
        const string FILE_PATH = "C:\\TestSite";

        public void SaveUserAdd(User user)
        {
            StringBuilder stringBuilder = new StringBuilder();
            Encrypt = new Encrypt();
            //Main props to save 

            stringBuilder.Append(Data.ConvertObjectToJson(user));
            ToTxt.AppendStringToTxt(FILE_PATH + FILE_NAME, Encrypt.EncryptString(stringBuilder.ToString(), "SkPRingsted", 5) + Environment.NewLine);
        }

        public void ReWriteUserFile(UserViewModel userViewModel)
        {
            List<string> itemsTosave = new List<string>();
            StringBuilder stringBuilder = new StringBuilder();
            //Main props to save
            foreach (var item in userViewModel.Users)
            {
                Encrypt = new Encrypt();
                stringBuilder.Append(Data.ConvertObjectToJson(item));
                itemsTosave.Add(Encrypt.EncryptString(stringBuilder.ToString(), "SkPRingsted"));
            }
            ToTxt.StringsToTxt(FILE_PATH + FILE_NAME, itemsTosave.ToArray());
        }

        public UserViewModel GetUsers()
        {
            UserViewModel userViewModel = new UserViewModel();
            try
            {
                string[] rawCourse = FromTxt.StringsFromTxt(FILE_PATH + FILE_NAME);

                foreach (string line in rawCourse)
                {
                    if (line != null)
                    {
                        Decrypt = new Decrypt();
                        string raw = Decrypt.DecryptString(line, "SkPRingsted", 5);

                        User user = (User)Data.ConvertJsonToObejct(raw, "User");
                        userViewModel.Users.Add(user);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new UserViewModel();
            }
            return userViewModel;
        }
    }
}
