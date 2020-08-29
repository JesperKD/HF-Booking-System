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


        #region Booking Methods
        public void SaveBooking(BookingViewModel booking)
        {
            Encrypt = new Encrypt();

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(booking.Id + "," + booking.CurrentUser.Id + "," + booking.CourseModel.Id + "#");
            foreach (var host in booking.HostsRentedForCourse)
            {
                stringBuilder.Append("," + host.Id);
            }
            stringBuilder.Append("+");
            ToTxt.AppendStringToTxt(FILE_PATH + FILE_NAME, Encrypt.EncryptString(stringBuilder.ToString(), "SkPRingsted", 5) + Environment.NewLine);
        }

        public void SaveAllBookings(List<BookingViewModel> bookingViewModels)
        {
            foreach (var item in bookingViewModels)
            {
                SaveBooking(item);
            }
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
                    string raw = Decrypt.DecryptString(line, "SkPRingsted", 5);
                    string[] bookingData = raw.Split('+');
                    foreach (var bookings in bookingData)
                    {
                        string[] bookingIds = bookings.Split('#');

                        BookingViewModel bookingViewModel = new BookingViewModel()
                        {
                            Id = int.Parse(bookingIds[0].Split(',')[0]),
                            CurrentUser = new User()
                            {
                                Id = int.Parse(bookingIds[0].Split(',')[1])
                            },
                            CourseModel = new Course()
                            {
                                Id = int.Parse(bookingIds[0].Split(',')[2])
                            },

                            HostsRentedForCourse = new List<Host>(),

                        };

                        foreach (var item in bookingIds[1].Split(',').ToList())
                        {
                            bookingViewModel.HostsRentedForCourse.Add( new Host ( ) { Id = int.Parse( item ) });
                        }
                    }

                }
                return bookingViewModels;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new List<BookingViewModel>();
            }
        }
        /// <summary>
        /// Takes the changed booking then make a match on the old one 
        /// It will delete the old and save the new 
        /// </summary>
        /// <param name="booking"></param>
        public void EditBooking(BookingViewModel booking)
        {
            List<BookingViewModel> bookingViewModels = GetBookings();
            
            // finds the old booking
            BookingViewModel bookingViewModelOld = bookingViewModels.Where(x => x.Id == booking.Id).FirstOrDefault();
            // Removes the old booking from the list
            bookingViewModels.Remove(bookingViewModelOld);
            
            //Inserts the changed booking
            bookingViewModels.Insert(booking.Id, booking);
            //Saves the remade list
            SaveAllBookings(bookingViewModels);
        }

        public void DeleteBooking(BookingViewModel booking)
        {
            List<BookingViewModel> bookingViewModels = GetBookings();

            // finds the old booking
            BookingViewModel bookingViewModelOld = bookingViewModels.Where(x => x.Id == booking.Id).FirstOrDefault();
            // Removes the old booking from the list
            bookingViewModels.Remove(bookingViewModelOld);

            //Saves the remade list
            SaveAllBookings(bookingViewModels);
        }
        #endregion

    }
}
