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
            List<string> itemsTosave = new List<string>();
            StringBuilder stringBuilder = new StringBuilder();
            Encrypt = new Encrypt();
            //Main props to save 
            foreach (var item in bookings)
            {
                stringBuilder.Append(Data.ConvertObjectToJson(item));
                itemsTosave.Add(Encrypt.EncryptString(stringBuilder.ToString(), "SkPRingsted", 5));
            }

            ToTxt.StringsToTxt(FILE_PATH + FILE_NAME, itemsTosave.ToArray());
        }

        public List<BookingViewModel> GetBookings()
        {
            List<BookingViewModel> bookingViewModels = new List<BookingViewModel>();
            try
            {
                string[] rawCourse = FromTxt.StringsFromTxt(FILE_PATH + FILE_NAME);

                foreach (string line in rawCourse)
                {
                    if (line != null)
                    {
                        Decrypt = new Decrypt();
                        string raw = Decrypt.DecryptString(line, "SkPRingsted", 5);

                        BookingViewModel bookingViewModel = (BookingViewModel)Data.ConvertJsonToObejct(raw, "BookingViewModel");
                        bookingViewModels.Add(bookingViewModel);
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
