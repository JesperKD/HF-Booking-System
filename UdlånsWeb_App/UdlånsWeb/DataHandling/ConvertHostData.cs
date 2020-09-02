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
            StringBuilder stringBuilder = new StringBuilder();
            //Main props to save
            foreach (var item in hostViewModel.Hosts)
            {
                Encrypt = new Encrypt();
                stringBuilder.Append(Data.ConvertObjectToJson(item));
                stringBuilder.Append("|");
            }

            ToTxt.StringToTxt(FILE_PATH + FILE_NAME, Encrypt.EncryptString(stringBuilder.ToString(), "SkPRingsted"));
        }

        public HostViewModel GetHosts()
        {
            HostViewModel hostViewModel = new HostViewModel();
            try
            {
                string[] rawCourse = FromTxt.StringsFromTxt(FILE_PATH + FILE_NAME);
                rawCourse.ToList();
                foreach (string line in rawCourse)
                {
                    Decrypt = new Decrypt();
                    string raw = Decrypt.DecryptString(line, "SkPRingsted");

                    foreach (var item in raw.Split("|"))
                    {
                        if (item.Length != 0)
                        {
                            Host host = (Host)Data.ConvertJsonToObejct(item, "Host");
                            hostViewModel.Hosts.Add(host);
                        }
                    }
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
