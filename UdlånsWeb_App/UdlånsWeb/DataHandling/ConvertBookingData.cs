using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdlånsWeb.Models;

namespace UdlånsWeb.DataHandling
{
    public class ConvertBookingData
    {
        ToTxt ToTxt = new ToTxt();
        FromTxt FromTxt = new FromTxt();
        Encrypt Encrypt;
        Decrypt Decrypt;
        const string FILE_NAME = "\\TestData.txt";
        const string FILE_PATH = "C:\\TestSite";


        public void SaveBookingAdd(BookingViewModel booking)
        {
            StringBuilder stringBuilder = new StringBuilder();
            Encrypt = new Encrypt();
            //Main props to save 

            stringBuilder.Append(Data.ConvertObjectToJson(booking));
            ToTxt.AppendStringToTxt(FILE_PATH + FILE_NAME, Encrypt.EncryptString(stringBuilder.ToString(), "SkPRingsted", 5) + Environment.NewLine);
        }

        public void RewriteBookingFile(List<BookingViewModel> bookings)
        {
            StringBuilder stringBuilder = new StringBuilder();
            //Main props to save 
            foreach (var booking in bookings)
            {
                Encrypt = new Encrypt();
                stringBuilder.Append(Data.ConvertObjectToJson(booking));
                stringBuilder.Append("|");
            }
            ToTxt.StringToTxt(FILE_PATH + FILE_NAME, Encrypt.EncryptString(stringBuilder.ToString(), "SkPRingsted"));
        }

        public List<BookingViewModel> GetBookings()
        {
            List<BookingViewModel> bookingViewModels = new List<BookingViewModel>();
            try
            {
                string[] rawCourse = FromTxt.StringsFromTxt(FILE_PATH + FILE_NAME);
                foreach (string line in rawCourse)
                {
                    Decrypt = new Decrypt();
                    string raw = Decrypt.DecryptString(line, "SkPRingsted");

                    foreach (var item in raw.Split("|"))
                    {
                        if (item.Length != 0)
                        {
                            BookingViewModel bookingViewModel = (BookingViewModel)Data.ConvertJsonToObejct(item, "BookingViewModel");
                            bookingViewModels.Add(bookingViewModel);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new List<BookingViewModel>();
            }
            return bookingViewModels;
        }
    }
}
