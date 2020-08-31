using Microsoft.Extensions.Hosting;
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
        const string FILE_NAME = "\\booking.txt";
        const string FILE_PATH = "C:\\TestSite";


        #region Course Methods
        public void SaveBooking(BookingViewModel booking)
        {
            Encrypt = new Encrypt();

            string hosts = string.Empty;
            foreach (Item Host in booking.HostRentedForCourse)
            {
                hosts = hosts + Host.HostName + "," + booking.Id + "," + Host.TurnInDate + ",";
            }
            if (hosts.EndsWith(','))
            {                
               hosts = hosts.Remove(hosts.Length - 1,1);
            }

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(booking.RentedClient + "," + hosts);
            ToTxt.AppendStringToTxt(FILE_PATH + FILE_NAME, Encrypt.EncryptString(stringBuilder.ToString(), "SkPRingsted", 5) + Environment.NewLine);
        }

        public List<BookingViewModel> GetBookings()
        {
            try
            {
                List<BookingViewModel> courseModel = new List<BookingViewModel>();

                string[] rawCourse = FromTxt.StringsFromTxt(FILE_PATH + FILE_NAME);

                foreach (string line in rawCourse)
                {
                    Decrypt = new Decrypt();
                    string raw = Decrypt.DecryptString(line, "SkPRingsted", 5);
                    string[] courseData = raw.Split(',');
                    BookingViewModel booking = new BookingViewModel();
                    booking.RentedClient = courseData[0];

                    booking.HostRentedForCourse.Add(new Item() { HostName = courseData[1], Id = int.Parse(courseData[2]), TurnInDate = DateTime.Parse(courseData[3]) });
                    if (courseData.Length > 4)
                    {
                        for (int i = 4; i < courseData.Length;)
                        {
                            var host = courseData.Skip(4).Take(3).ToArray();
                            booking.HostRentedForCourse.Add(new Item() { HostName = host[0], Id = int.Parse(host[1]), TurnInDate = DateTime.Parse(host[2]) });
                            i += 3;
                        }
                    }


                    //booking.HostRentedForCourse.HostName = courseData[1];
                    //booking.Id = int.Parse(courseData[2]);
                    //booking.HostRentedForCourse.TurnInDate = DateTime.Parse(courseData[3]);
                    courseModel.Add(booking);

                }
                return courseModel;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new List<BookingViewModel>();
            }
        }

        public void EditBooking(BookingViewModel booking)
        {
            //Logic for Edit Item
            var courseModelOld = new List<BookingViewModel>();

            // gets all items from file
            string[] rawCourse = FromTxt.StringsFromTxt(FILE_PATH + FILE_NAME);

            foreach (string itemLine in rawCourse)
            {
                Decrypt = new Decrypt();
                string raw = Decrypt.DecryptString(itemLine, "SkPRingsted", 5);
                string[] courseData = raw.Split(',');
                BookingViewModel oCourse = new BookingViewModel();

                booking.RentedClient = courseData[0];

                booking.HostRentedForCourse.Add(new Item() { HostName = courseData[1], Id = int.Parse(courseData[2]), TurnInDate = DateTime.Parse(courseData[3]) });
                if (courseData.Length > 4)
                {
                    for (int i = 4; i < courseData.Length;)
                    {
                        var host = courseData.Skip(4).Take(3).ToArray();
                        booking.HostRentedForCourse.Add(new Item() { HostName = host[0], Id = int.Parse(host[1]), TurnInDate = DateTime.Parse(host[2]) });
                        i += 3;
                    }
                }

                //oCourse.RentedClient = courseData[0];
                //oCourse.HostRentedForCourse.HostName = courseData[1];
                //oCourse.Id = int.Parse(courseData[2]);
                courseModelOld.Add(oCourse);
            }

            // finds the old item and removes it
            BookingViewModel OldCourse = courseModelOld.Where(x => x.RentedClient == booking.RentedClient).FirstOrDefault();
            courseModelOld.Remove(OldCourse);

            // creates new list from old, and inserts edited item at index Id
            List<BookingViewModel> bookingModelNew = new List<BookingViewModel>();
            bookingModelNew = courseModelOld;
            bookingModelNew.Insert(booking.Id, booking);


            // creates correct item string
            List<string> coursesTosave = new List<string>();

            // makes each item into a new string
            foreach (var xbooking in bookingModelNew)
            {
                string hosts = string.Empty;
                foreach (Item Host in xbooking.HostRentedForCourse)
                {
                    hosts += Host.HostName + "," + booking.Id + "," + Host.TurnInDate + ",";
                }
                if (hosts.EndsWith(','))
                {
                    hosts.TrimEnd(',');
                }

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(booking.RentedClient + "," + hosts);

                Encrypt = new Encrypt();
                coursesTosave.Add(Encrypt.EncryptString(stringBuilder.ToString(), "SkPRingsted", 5));
            }
            // overrides file with new strings
            ToTxt.StringsToTxt(FILE_PATH + FILE_NAME, coursesTosave.ToArray());
        }

        public void DeleteBooking(BookingViewModel booking)
        {
            // Code input item that has to be deleted 
            var courseModel = new List<BookingViewModel>();
            try
            {
                // gets all users from file
                string[] rawCourse = FromTxt.StringsFromTxt(FILE_PATH + FILE_NAME);

                foreach (string Line in rawCourse)
                {
                    Decrypt = new Decrypt();
                    string raw = Decrypt.DecryptString(Line, "SkPRingsted", 5);
                    string[] courseData = raw.Split(',');
                    Models.BookingViewModel oCourse = new BookingViewModel();
                    oCourse.RentedClient = courseData[0];
                    //oCourse.HostRentedForCourse.HostName = courseData[1];

                    courseModel.Add(oCourse);
                }

            }
            catch (Exception)
            {

            }

            // finds the old item and removes it
            BookingViewModel removeCourse = courseModel.Where(x => x.RentedClient == booking.RentedClient).First();
            courseModel.Remove(removeCourse);

            // creates correct user string
            List<string> coursesTosave = new List<string>();

            foreach (var bookingx in courseModel)
            {
                string hosts = string.Empty;
                foreach (Item Host in bookingx.HostRentedForCourse)
                {
                    hosts += Host.HostName + "," + bookingx.Id + "," + Host.TurnInDate + ",";
                }
                if (hosts.EndsWith(','))
                {
                    hosts.TrimEnd(',');
                }

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(bookingx.RentedClient + "," + hosts);

                //StringBuilder stringBuilder = new StringBuilder();
                //stringBuilder.Append(booking.RentedClient + "," + booking.HostRentedForCourse.HostName + "," + booking.Id);

                Encrypt = new Encrypt();
                coursesTosave.Add(Encrypt.EncryptString(stringBuilder.ToString(), "SkPRingsted", 5));
            }

            // overrides file with new strings
            ToTxt.StringsToTxt(FILE_PATH + FILE_NAME, coursesTosave.ToArray());
        }
        #endregion	        #endregion

    }
}
