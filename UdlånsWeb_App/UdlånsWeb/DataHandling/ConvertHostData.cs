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
        public void ReWriteHostFile(HostViewModel hostViewModel)
        {
            StringBuilder stringBuilder = new StringBuilder();
            //Main props to save
            Encrypt = new Encrypt();
            stringBuilder.Append(Data.ConvertObjectToJson(hostViewModel));

            ToTxt.StringToTxt(FILE_PATH + FILE_NAME, Encrypt.EncryptString(stringBuilder.ToString(), "SkPRingsted"));
        }

        public HostViewModel GetHosts()
        {
            HostViewModel hostViewModel = new HostViewModel();
            try
            {
                string[] rawFile = FromTxt.StringsFromTxt(FILE_PATH + FILE_NAME);

                Decrypt = new Decrypt();
                string raw = Decrypt.DecryptString(rawFile[0], "SkPRingsted");

                hostViewModel = (HostViewModel)Data.ConvertJsonToObejct(raw, "HostViewModel");

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
