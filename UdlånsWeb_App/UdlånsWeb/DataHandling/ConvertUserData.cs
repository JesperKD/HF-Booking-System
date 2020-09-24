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
        PasswordGenerator passwordGenerator = new PasswordGenerator();
        const string USER_FILE_NAME = "\\user.txt";
        const string FILE_PATH = "C:\\TestSite";

        public void AddUser(User user)
        {
            Encrypt = new Encrypt();
            //Save the user to file/database
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(user.Name + "," + user.Initials + "," + user.Email + "," + user.Admin + "," + 0 + "," + user.Password);

            // change to correct path for file saving
            ToTxt.AppendStringToTxt(FILE_PATH + USER_FILE_NAME, Encrypt.EncryptString(stringBuilder.ToString(), "SkPRingsted", 5) + Environment.NewLine);
        }

        public UserViewModel GetUsers()
        {
            try
            {
                var userModel = new UserViewModel();

                // rewrite to handle decryption
                string[] rawUser = FromTxt.StringsFromTxt(FILE_PATH + USER_FILE_NAME);

                foreach (string line in rawUser)
                {
                    Decrypt = new Decrypt();
                    string raw = Decrypt.DecryptString(line, "SkPRingsted", 5);
                    string[] userData = raw.Split(',');
                    User user = new User();
                    user.Name = userData[0];
                    user.Initials = userData[1].ToUpper();
                    user.Email = userData[2];
                    user.Admin = Convert.ToBoolean(userData[3]);
                    user.Id = int.Parse(userData[4]);
                    user.Password = userData[5];
                    userModel.Users.Add(user);

                }
                return userModel;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public User GetCurrentUser(string initials)
        {
            User currentUser = new User();

            try
            {
                var userModel = new UserViewModel();
                string[] rawUser = FromTxt.StringsFromTxt(FILE_PATH + USER_FILE_NAME);

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
                    user.Password = userData[5];
                    userModel.Users.Add(user);

                }

                currentUser = userModel.Users.Where(x => x.Initials.ToUpper() == initials).FirstOrDefault();

                return currentUser;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public void EditUser(User user)
        {
            //Logic for Edit User

            var userModelOld = new UserViewModel();

            // gets all users from file
            string[] rawUser = FromTxt.StringsFromTxt(FILE_PATH + USER_FILE_NAME);

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
                oUser.Password = userData[5];
                userModelOld.Users.Add(oUser);
            }

            // finds the old user and removes it
            User OldUser = userModelOld.Users[user.Id];
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
                stringBuilder.Append(Item.Name + "," + Item.Initials + "," + Item.Email + "," + Item.Admin + "," + Item.Id + "," + Item.Password);

                Encrypt = new Encrypt();
                usersTosave.Add(Encrypt.EncryptString(stringBuilder.ToString(), "SkPRingsted", 5));
                // change to correct path for file saving
            }
            // overrides file with new strings
            ToTxt.StringsToTxt(FILE_PATH + USER_FILE_NAME, usersTosave.ToArray());
        }

        public void DeleteUser(User user)
        {
            // Code input user that has to be deleted 

            var userModel = new UserViewModel();
            try
            {
                // gets all users from file
                string[] rawUser = FromTxt.StringsFromTxt(FILE_PATH + USER_FILE_NAME);

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
                    oUser.Password = userData[5];
                    userModel.Users.Add(oUser);
                }

            }
            catch (Exception)
            {

            }

            // finds the old user and removes it
            User removeUser = userModel.Users.Where(x => x.Name == user.Name && x.Email == user.Email).FirstOrDefault();
            userModel.Users.Remove(removeUser);

            // creates correct user string
            List<string> usersTosave = new List<string>();

            foreach (User Item in userModel.Users)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(Item.Name + "," + Item.Initials + "," + Item.Email + "," + Item.Admin + "," + Item.Id + "," + Item.Password);

                Encrypt = new Encrypt();
                usersTosave.Add(Encrypt.EncryptString(stringBuilder.ToString(), "SkPRingsted", 5));
                // change to correct path for file saving
            }
            // overrides file with new strings
            ToTxt.StringsToTxt(FILE_PATH + USER_FILE_NAME, usersTosave.ToArray());
        }
    }
}
