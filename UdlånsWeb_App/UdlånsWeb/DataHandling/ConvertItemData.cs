using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UdlånsWeb.Models;

namespace UdlånsWeb.DataHandling
{
    public class ConvertItemData
    {
        ToTxt ToTxt = new ToTxt();
        FromTxt FromTxt = new FromTxt();
        Encrypt Encrypt;
        Decrypt Decrypt;
        const string ITEM_FILE_NAME = "\\item.txt";
        const string FILE_PATH = "C:\\TestSite";

        public void AddItem(Item item)
        {
            Encrypt = new Encrypt();

            int itemID = 0;
            try
            {
                ItemViewModel items = GetItems();
                if (items != null && items.Items.Count > 0)
                {
                    itemID = items.Items.Last().Id + 1;
                }
            }
            catch (Exception)
            {

            }
            item.RentedDate = DateTime.MinValue;

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(item.HostName + "," + item.HostPassword + "," + item.UserName + "," + item.VmWareVersion + "," + item.HostIp + "," + "," + item.Rented + "," + itemID
                + "," + item.TurnInDate + "," + item.InUse + "," + item.RentedDate + "," + item.Description);

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
                    item.Rented = Convert.ToBoolean(itemData[6]);
                    item.Id = int.Parse(itemData[7]);
                    item.InUse = bool.Parse(itemData[9]);
                    item.RentedDate = DateTime.Parse(itemData[10]);
                    item.Description = itemData[11];
                    if (itemData[6] != null) item.TurnInDate = DateTime.Parse(itemData[8]);

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
                oItem.Rented = Convert.ToBoolean(itemData[6]);
                oItem.Id = int.Parse(itemData[7]);
                oItem.InUse = bool.Parse(itemData[9]);
                oItem.RentedDate = DateTime.Parse(itemData[10]);
                oItem.Description = itemData[11];
                if (itemData[6] != null) oItem.TurnInDate = DateTime.Parse(itemData[8]);

                itemModelOld.Items.Add(oItem);
            }

            // finds the old item and removes it
            Item OldItem = itemModelOld.Items.Where(x => x.Id == item.Id).FirstOrDefault();
            itemModelOld.Items.Remove(OldItem);

            // creates new list from old, and inserts edited item at index Id
            ItemViewModel ItemModelNew = new ItemViewModel();
            ItemModelNew = itemModelOld;


            ItemModelNew.Items.Add(item);

            ItemModelNew.Items = ItemModelNew.Items.OrderBy(x => x.Id).ToList();

            // creates correct item string
            List<string> itemsTosave = new List<string>();

            // makes each item into a new string
            foreach (Item Item in ItemModelNew.Items)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(Item.HostName + "," + Item.HostPassword + "," + Item.UserName + "," + Item.VmWareVersion + "," + Item.HostIp + ","
                    + "," + Item.Rented + "," + Item.Id + "," + Item.TurnInDate + "," + Item.InUse + "," + Item.RentedDate + ","+Item.Description);

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
                    oItem.Rented = Convert.ToBoolean(itemData[6]);
                    oItem.Id = int.Parse(itemData[7]);
                    oItem.InUse = bool.Parse(itemData[9]);
                    oItem.RentedDate = DateTime.Parse(itemData[10]);
                    oItem.Description = itemData[11];
                    if (itemData[6] != null) oItem.TurnInDate = DateTime.Parse(itemData[8]);
                    itemModel.Items.Add(oItem);
                }

            }
            catch (Exception)
            {

            }

            // finds the old item and removes it
            Item removeUser = itemModel.Items.Where(x => x.Id == item.Id).FirstOrDefault();
            itemModel.Items.Remove(removeUser);

            // creates correct user string
            List<string> itemsTosave = new List<string>();

            //int id = 0;
            foreach (Item host in itemModel.Items)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(host.HostName + "," + host.HostPassword + "," + host.UserName + "," + host.VmWareVersion + "," + host.HostIp + "," + "," + host.Rented
                    + "," + host.Id + "," + host.TurnInDate + "," + host.InUse + "," + host.RentedDate + "," + host.Description);

                Encrypt = new Encrypt();
                itemsTosave.Add(Encrypt.EncryptString(stringBuilder.ToString(), "SkPRingsted", 5));
                //id++;
            }

            // overrides file with new strings
            ToTxt.StringsToTxt(FILE_PATH + ITEM_FILE_NAME, itemsTosave.ToArray());
        }
    }
}
