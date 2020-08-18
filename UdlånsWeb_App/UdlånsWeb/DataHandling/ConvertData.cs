using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdlånsWeb.Models;

namespace UdlånsWeb.DataHandling
{
    public class ConvertData
    {
        ToTxt ToTxt = new ToTxt();
        FromTxt FromTxt = new FromTxt();
        Encrypt Encrypt;
        Decrypt Decrypt;
        string fileName { get; set; } = "\\user.txt";
        string path { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public void AddUser(User user)
        {
            Encrypt = new Encrypt();
            //Save the user to file/database

            // rewrite to handle encryption
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(user.Name + "," + user.Initials + "," + user.Email + "," + user.Admin + "," + 0);

            // change to correct path for file saving
            ToTxt.AppendStringToTxt(path + fileName, Encrypt.EncryptString(stringBuilder.ToString(), "SkPRingsted", 5) + Environment.NewLine);
        }
        string[] rawUser { get; set; } = new string[1];

        public UserViewModel GetUsers()
        {
            var userModel = new UserViewModel();
            try
            {
                // rewrite to handle decryption
                rawUser = FromTxt.StringsFromTxt(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\user.txt");
                Console.WriteLine(rawUser.Length);

            }
            catch (Exception)
            {
                Console.WriteLine(rawUser.Length);
            }

            if (string.IsNullOrEmpty(rawUser[0])) return userModel;

            foreach (string line in rawUser)
            {

                Decrypt = new Decrypt();
                string raw = Decrypt.DecryptString(line, "SkPRingsted", 5);
                string[] userData = raw.Split(',');
                User user = new User();
                user.Name = userData[0];
                user.Initials = userData[1];
                user.Email = userData[2];
                user.Admin = Convert.ToBoolean(userData[3]);
                user.Id = int.Parse(userData[4]);
                userModel.Users.Add(user);
            }
            return userModel;
        }

        public void EditUser(User user)
        {
            //Logic for Edit User

            var userModelOld = new UserViewModel();

            // gets all users from file
            string[] rawUser = FromTxt.StringsFromTxt(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\user.txt");

            foreach (string userLine in rawUser)
            {
                Decrypt = new Decrypt();
                string raw = Decrypt.DecryptString(userLine, "SkPRingsted", 5);
                string[] userData = raw.Split(',');
                Models.User oUser = new User();
                oUser.Name = userData[0];
                oUser.Initials = userData[1];
                oUser.Email = userData[2];
                oUser.Admin = Convert.ToBoolean(userData[3]);
                oUser.Id = int.Parse(userData[4]);
                userModelOld.Users.Add(oUser);
            }

            // finds the old user and removes it
            User OldUser = userModelOld.Users.Where(x => x.Id == user.Id).FirstOrDefault();
            userModelOld.Users.Remove(OldUser);

            // creates new list from old, and inserts edited user at index Id
            UserViewModel userModelNew = new UserViewModel();
            userModelNew = userModelOld;
            userModelNew.Users.Insert(user.Id, user);

            // creates correct user string
            List<string> usersTosave = new List<string>();

            // makes each user into a new string
            foreach (User Item in userModelNew.Users)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(Item.Name + "," + Item.Initials + "," + Item.Email + "," + Item.Admin + "," + Item.Id);

                Encrypt = new Encrypt();
                usersTosave.Add(Encrypt.EncryptString(stringBuilder.ToString(), "SkPRingsted", 5));
                // change to correct path for file saving
            }
            // overrides file with new strings
            ToTxt.StringsToTxt(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\user.txt", usersTosave.ToArray());
        }

        public void DeleteUser(User user)
        {
            // Code input user that has to be deleted 

            var userModel = new UserViewModel();

            // gets all users from file
            string[] rawUser = FromTxt.StringsFromTxt(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\user.txt");

            foreach (string userLine in rawUser)
            {
                Decrypt = new Decrypt();
                string raw = Decrypt.DecryptString(userLine, "SkPRingsted", 5);
                string[] userData = raw.Split(',');
                Models.User oUser = new User();
                oUser.Name = userData[0];
                oUser.Initials = userData[1];
                oUser.Email = userData[2];
                oUser.Admin = Convert.ToBoolean(userData[3]);
                oUser.Id = int.Parse(userData[4]);
                userModel.Users.Add(oUser);
            }

            // finds the old user and removes it
            User removeUser = userModel.Users.Where(x => x.Name == user.Name && x.Initials == user.Initials && x.Email == user.Email).First();
            userModel.Users.Remove(removeUser);

            // creates correct user string
            List<string> usersTosave = new List<string>();

            foreach (User Item in userModel.Users)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(Item.Name + "," + Item.Initials + "," + Item.Email + "," + Item.Admin + "," + Item.Id);

                usersTosave.Add(Encrypt.EncryptString(stringBuilder.ToString(), "SkPRingsted", 5));
                // change to correct path for file saving
            }
            // overrides file with new strings
            ToTxt.StringsToTxt(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\user.txt", usersTosave.ToArray());
        }
    }
}
