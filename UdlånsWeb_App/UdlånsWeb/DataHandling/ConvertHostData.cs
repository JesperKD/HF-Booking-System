using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdlånsWeb.Models;

namespace UdlånsWeb.DataHandling
{
    public class ConvertHostData
    {
        ToTxt ToTxt = new ToTxt();
        FromTxt FromTxt = new FromTxt();
        Encrypt Encrypt;
        Decrypt Decrypt;
        const string ITEM_FILE_NAME = "\\host.txt";
        const string FILE_PATH = "C:\\TestSite";

        public void AddHost(Host host)
        {
            Encrypt = new Encrypt();

            HostViewModel hostsView = GetHosts();
            hostsView.Items.Sort();
            int highestNumberForId = 0;
             if(hostsView.Items.Contains(host))
            foreach (var item in hostsView.Items)
            {
                if(item.Id > highestNumberForId)
                {
                    highestNumberForId = item.Id;
                }
            }

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(host.Name + "," + host.Password + "," + host.UserName + "," + host.VmWareVersion + "," + host.HostIp + "," + host.NumberOfPeoplePerHost + "," + host.Rented + "," + highestNumberForId + "," + host.TurnInDate);

            ToTxt.AppendStringToTxt(FILE_PATH + ITEM_FILE_NAME, Encrypt.EncryptString(stringBuilder.ToString(), "SkPRingsted", 5) + Environment.NewLine);
        }

        public HostViewModel GetHosts()
        {
            try
            {
                var itemModel = new HostViewModel();

                string[] rawItem = FromTxt.StringsFromTxt(FILE_PATH + ITEM_FILE_NAME);

                foreach (string line in rawItem)
                {
                    Decrypt = new Decrypt();
                    string raw = Decrypt.DecryptString(line, "SkPRingsted", 5);
                    string[] itemData = raw.Split(',');
                    Host item = new Host();
                    item.Name = itemData[0];
                    item.Password = itemData[1];
                    item.UserName = itemData[2];
                    item.VmWareVersion = itemData[3];
                    item.HostIp = itemData[4];
                    item.NumberOfPeoplePerHost = int.Parse(itemData[5]);
                    item.Rented = Convert.ToBoolean(itemData[6]);
                    item.Id = int.Parse(itemData[7]);
                    if (itemData[8] != null) item.TurnInDate = DateTime.Parse(itemData[8]);

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

        public void EditHost(Host item)
        {
            //Logic for Edit Item
            var itemModelOld = new HostViewModel();

            // gets all items from file
            string[] rawItem = FromTxt.StringsFromTxt(FILE_PATH + ITEM_FILE_NAME);

            foreach (string itemLine in rawItem)
            {
                Decrypt = new Decrypt();
                string raw = Decrypt.DecryptString(itemLine, "SkPRingsted", 5);
                string[] itemData = raw.Split(',');
                Models.Host oItem = new Host();
                oItem.Name = itemData[0];
                oItem.Password = itemData[1];
                oItem.UserName = itemData[2];
                oItem.VmWareVersion = itemData[3];
                oItem.HostIp = itemData[4];
                oItem.NumberOfPeoplePerHost = int.Parse(itemData[5]);
                oItem.Rented = Convert.ToBoolean(itemData[6]);
                oItem.Id = int.Parse(itemData[7]);
                if (itemData[8] != null) oItem.TurnInDate = DateTime.Parse(itemData[8]);

                itemModelOld.Items.Add(oItem);
            }

            // finds the old host and removes it
            Host OldItem = itemModelOld.Items.Where(x => x.Id == item.Id).FirstOrDefault();
            itemModelOld.Items.Remove(OldItem);

            // creates new list from old, and inserts edited host at index Id
            HostViewModel ItemModelNew = new HostViewModel();
            ItemModelNew = itemModelOld;
            ItemModelNew.Items.Insert(item.Id, item);

            // creates correct host string
            List<string> itemsTosave = new List<string>();

            // makes each host into a new string
            foreach (Host Item in ItemModelNew.Items)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(Item.Name + "," + Item.Password + "," + Item.UserName + "," + Item.VmWareVersion + "," + Item.HostIp + "," + Item.NumberOfPeoplePerHost + "," + Item.Rented + "," + Item.Id + "," + Item.TurnInDate);

                Encrypt = new Encrypt();
                itemsTosave.Add(Encrypt.EncryptString(stringBuilder.ToString(), "SkPRingsted", 5));
            }
            // overrides file with new strings
            ToTxt.StringsToTxt(FILE_PATH + ITEM_FILE_NAME, itemsTosave.ToArray());
        }

        public void DeleteHost(Host host)
        {
            // Code input host that has to be deleted 
            var itemModel = new HostViewModel();
            try
            {
                // gets all users from file
                string[] rawItem = FromTxt.StringsFromTxt(FILE_PATH + ITEM_FILE_NAME);

                foreach (string itemLine in rawItem)
                {
                    Decrypt = new Decrypt();
                    string raw = Decrypt.DecryptString(itemLine, "SkPRingsted", 5);
                    string[] itemData = raw.Split(',');
                    Models.Host oItem = new Host();
                    oItem.Name = itemData[0];
                    oItem.Password = itemData[1];
                    oItem.UserName = itemData[2];
                    oItem.VmWareVersion = itemData[3];
                    oItem.HostIp = itemData[4];
                    oItem.NumberOfPeoplePerHost = int.Parse(itemData[5]);
                    oItem.Rented = Convert.ToBoolean(itemData[6]);
                    oItem.Id = int.Parse(itemData[7]);
                    itemModel.Items.Add(oItem);
                }
            }
            catch (Exception)
            {

            }

            // finds the old host and removes it
            Host removeUser = itemModel.Items.Where(x => x.Name == host.Name && x.HostIp == host.HostIp && x.Id == host.Id).FirstOrDefault();
            itemModel.Items.Remove(removeUser);

            // creates correct user string
            List<string> itemsTosave = new List<string>();

            foreach (Host Item in itemModel.Items)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(host.Name + "," + host.Password + "," + host.UserName + "," + host.VmWareVersion + "," + host.HostIp + "," + host.NumberOfPeoplePerHost + "," + host.Rented);

                Encrypt = new Encrypt();
                itemsTosave.Add(Encrypt.EncryptString(stringBuilder.ToString(), "SkPRingsted", 5));
            }

            // overrides file with new strings
            ToTxt.StringsToTxt(FILE_PATH + ITEM_FILE_NAME, itemsTosave.ToArray());
        }
    }
}
