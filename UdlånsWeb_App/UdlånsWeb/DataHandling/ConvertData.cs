using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        const string USER_FILE_NAME = "\\user.txt";
        const string ITEM_FILE_NAME = "\\item.txt";
        const string FILE_PATH = "C:\\TestSite";
        public void AddUser(User user)
        {
            Encrypt = new Encrypt();
            //Save the user to file/database

            // rewrite to handle encryption
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(user.Name + "," + user.Initials + "," + user.Email + "," + user.Admin + "," + 0);

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
                    user.Initials = userData[1];
                    user.Email = userData[2];
                    user.Admin = Convert.ToBoolean(userData[3]);
                    user.Id = int.Parse(userData[4]);
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
                    userModel.Users.Add(oUser);
                }

            }
            catch (Exception)
            {

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

                Encrypt = new Encrypt();
                usersTosave.Add(Encrypt.EncryptString(stringBuilder.ToString(), "SkPRingsted", 5));
                // change to correct path for file saving
            }
            // overrides file with new strings
            ToTxt.StringsToTxt(FILE_PATH + USER_FILE_NAME, usersTosave.ToArray());
        }

        public void AddItem(Item item)
        {
            Encrypt = new Encrypt();

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(item.HostName + "," + item.HostPassword + "," + item.UserName + "," + item.VmWareVersion + "," + item.HostIp + "," + item.NumberOfPeoplePerHost + "," + item.Rented);

            ToTxt.AppendStringToTxt(FILE_PATH + ITEM_FILE_NAME, Encrypt.EncryptString(stringBuilder.ToString(), "SkPRingsted", 5) + Environment.NewLine);
        }

        public ItemViewModel GetItems()
        {
            try
            {
                var itemModel = new ItemViewModel();

                string[] rawItem = FromTxt.StringsFromTxt(FILE_PATH + ITEM_FILE_NAME);

                foreach (string line in rawItem)
                {
                    Decrypt = new Decrypt();
                    string raw = Decrypt.DecryptString(line, "SkPRingsted", 5);
                    string[] itemData = raw.Split(',');
                    Item item = new Item();
                    item.HostName = itemData[0];
                    item.HostPassword = itemData[1];
                    item.UserName = itemData[2];
                    item.VmWareVersion = itemData[3];
                    item.HostIp = itemData[4];
                    item.NumberOfPeoplePerHost = int.Parse(itemData[5]);
                    item.Rented = Convert.ToBoolean(itemData[6]);

                    itemModel.Items.Add(item);

                }
                return itemModel;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public void EditItem(Item item)
        {
            //Logic for Edit Item
            var itemModelOld = new ItemViewModel();

            // gets all items from file
            string[] rawItem = FromTxt.StringsFromTxt(FILE_PATH + ITEM_FILE_NAME);

            foreach (string itemLine in rawItem)
            {
                Decrypt = new Decrypt();
                string raw = Decrypt.DecryptString(itemLine, "SkPRingsted", 5);
                string[] itemData = raw.Split(',');
                Models.Item oItem = new Item();
                oItem.HostName = itemData[0];
                oItem.HostPassword = itemData[1];
                oItem.UserName = itemData[2];
                oItem.VmWareVersion = itemData[3];
                oItem.HostIp = itemData[4];
                oItem.NumberOfPeoplePerHost = int.Parse(itemData[5]);
                oItem.Rented = Convert.ToBoolean(itemData[6]);
                itemModelOld.Items.Add(oItem);
            }

            // finds the old item and removes it
            Item OldItem = itemModelOld.Items.Where(x => x.Id == item.Id).FirstOrDefault();
            itemModelOld.Items.Remove(OldItem);

            // creates new list from old, and inserts edited item at index Id
            ItemViewModel ItemModelNew = new ItemViewModel();
            ItemModelNew = itemModelOld;
            ItemModelNew.Items.Insert(item.Id, item);

            // creates correct item string
            List<string> itemsTosave = new List<string>();

            // makes each item into a new string
            foreach (Item Item in ItemModelNew.Items)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(item.HostName + "," + item.HostPassword + "," + item.UserName + "," + item.VmWareVersion + "," + item.HostIp + "," + item.NumberOfPeoplePerHost + "," + item.Rented + "," + item.Id);

                Encrypt = new Encrypt();
                itemsTosave.Add(Encrypt.EncryptString(stringBuilder.ToString(), "SkPRingsted", 5));
            }
            // overrides file with new strings
            ToTxt.StringsToTxt(FILE_PATH + ITEM_FILE_NAME, itemsTosave.ToArray());
        }

        public void DeleteItem(Item item)
        {
            // Code input item that has to be deleted 
            var itemModel = new ItemViewModel();
            try
            {
                // gets all users from file
                string[] rawItem = FromTxt.StringsFromTxt(FILE_PATH + ITEM_FILE_NAME);

                foreach (string itemLine in rawItem)
                {
                    Decrypt = new Decrypt();
                    string raw = Decrypt.DecryptString(itemLine, "SkPRingsted", 5);
                    string[] itemData = raw.Split(',');
                    Models.Item oItem = new Item();
                    oItem.HostName = itemData[0];
                    oItem.HostPassword = itemData[1];
                    oItem.UserName = itemData[2];
                    oItem.VmWareVersion = itemData[3];
                    oItem.HostIp = itemData[4];
                    oItem.NumberOfPeoplePerHost = int.Parse(itemData[5]);
                    oItem.Rented = Convert.ToBoolean(itemData[6]);
                    itemModel.Items.Add(oItem);
                }

            }
            catch (Exception)
            {

            }

            // finds the old item and removes it
            Item removeUser = itemModel.Items.Where(x => x.HostName == item.HostName && x.HostIp == item.HostIp && x.Id == item.Id).First();
            itemModel.Items.Remove(removeUser);
            
            //foreach(Item itemx in itemModel.Items)
            //{
            //    if(itemx.HostName == item.HostName && itemx.HostIp == item.HostIp)
            //    {
            //        itemModel.Items.Remove(itemx);
            //    }
            //}

            // creates correct user string
            List<string> itemsTosave = new List<string>();

            foreach (Item Item in itemModel.Items)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(item.HostName + "," + item.HostPassword + "," + item.UserName + "," + item.VmWareVersion + "," + item.HostIp + "," + item.NumberOfPeoplePerHost + "," + item.Rented);

                Encrypt = new Encrypt();
                itemsTosave.Add(Encrypt.EncryptString(stringBuilder.ToString(), "SkPRingsted", 5));
            }
            
            // overrides file with new strings
            ToTxt.StringsToTxt(FILE_PATH + ITEM_FILE_NAME, itemsTosave.ToArray());
        }
    }
}
