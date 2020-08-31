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
        const string FILE_NAME = "\\host.txt";
        const string FILE_PATH = "C:\\TestSite";

        public void SaveHostAdd(Host host)
        {
            StringBuilder stringBuilder = new StringBuilder();
            Encrypt = new Encrypt();
            //Main props to save 

            stringBuilder.Append(Data.ConvertObjectToJson(host));
            ToTxt.AppendStringToTxt(FILE_PATH + FILE_NAME, Encrypt.EncryptString(stringBuilder.ToString(), "SkPRingsted", 5) + Environment.NewLine);
        }

        public void ReWriteHostFile(HostViewModel hostViewModel)
        {
            List<string> itemsTosave = new List<string>();
            StringBuilder stringBuilder = new StringBuilder();
            Encrypt = new Encrypt();
            //Main props to save
            foreach (var item in hostViewModel.Hosts)
            {
                stringBuilder.Append(Data.ConvertObjectToJson(item));
                itemsTosave.Add(Encrypt.EncryptString(stringBuilder.ToString(), "SkPRingsted", 5));
            }
            ToTxt.StringsToTxt(FILE_PATH + FILE_NAME, itemsTosave.ToArray());
        }

        public HostViewModel GetHosts()
        {
            HostViewModel hostViewModel = new HostViewModel();
            try
            {
                string[] rawCourse = FromTxt.StringsFromTxt(FILE_PATH + FILE_NAME);

                foreach (string line in rawCourse)
                {
                    Decrypt = new Decrypt();
                    string raw = Decrypt.DecryptString(line, "SkPRingsted", 5);

                    Host host = (Host)Data.ConvertJsonToObejct(raw, "Host");
                    hostViewModel.Hosts.Add(host);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new HostViewModel();
            }
            return hostViewModel;
        }
    }
}
