using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdlånsWeb.Models;

namespace UdlånsWeb.DataHandling
{
    public static class Data
    {
        private static ConvertCourseData convertCourseData { get; set; } = new ConvertCourseData();
        private static ConvertHostData convertHostData { get; set; } = new ConvertHostData();
        private static ConvertUserData convertUserData { get; set; } = new ConvertUserData();
        private static ConvertLoginData convertlogindata { get; set; } = new ConvertLoginData();
        private static ConvertBookingData convertBookingData { get; set; } = new ConvertBookingData();

        public static CourseViewModel CourseViewModel { get; set; }
        public static HostViewModel HostViewModel { get; set; }
        public static UserViewModel UserViewModel { get; set; }
        public static List<BookingViewModel> BookingViewModels { get; set; }

        #region Host
        public static HostViewModel GetHosts()
        {
            HostViewModel = convertHostData.GetHosts();
            return HostViewModel;
        }
        /// <summary>
        /// Make sure to add the host to Data.HostViewModel.Hosts
        /// </summary>
        public static void SaveHosts()
        {
            convertHostData.ReWriteHostFile(convertHostData.GetHosts());
        }
        /// <summary>
        /// Deletes the host from the file
        /// </summary>
        /// <param name="host"></param>
        public static void DeleteHost(Host host)
        {
            //Get the hosts from the file
            HostViewModel = convertHostData.GetHosts();
            //Finds the user getting deleted
            Host hostGettingDeleted = HostViewModel.Hosts.Where(x => x.Id == host.Id).FirstOrDefault();
            //Deletes the old host data from the file
            HostViewModel.Hosts.Remove(hostGettingDeleted);
            //Overrides the file with all the hosts
            convertHostData.ReWriteHostFile(HostViewModel);
        }
        public static void EditHost(Host host)
        {
            //Get the hosts from the file
            HostViewModel = convertHostData.GetHosts();
            //Finds the user getting edited
            Host hostGettingDeleted = HostViewModel.Hosts.Where(x => x.Id == host.Id).FirstOrDefault();
            //Deletes the old host data from the file
            HostViewModel.Hosts.Remove(hostGettingDeleted);
            //Adds the new edited host to the host view model
            HostViewModel.Hosts.Add(host);
            //Overrides the file with all the hosts
            convertHostData.ReWriteHostFile(HostViewModel);
        }
        #endregion
        #region Booking
        public static List<BookingViewModel> GetBookings()
        {
            BookingViewModels = convertBookingData.GetBookings();
            return BookingViewModels;
        }
        public static void SaveBookings()
        {
            convertBookingData.RewriteBookingFile(BookingViewModels);
        }
        public static void DeleteBooking(BookingViewModel bookingViewModel)
        {
            BookingViewModel modelToDelete = GetBookings().Where(x => x.Id == bookingViewModel.Id).FirstOrDefault();
            BookingViewModels.Remove(modelToDelete);
            SaveBookings();
        }
        public static void EditBooking(BookingViewModel bookingViewModel)
        {
            BookingViewModel modelToDelete = GetBookings().Where(x => x.Id == bookingViewModel.Id).FirstOrDefault();
            BookingViewModels.Remove(modelToDelete);
            BookingViewModels.Add(bookingViewModel);
            SaveBookings();
        }
        #endregion


        /// <summary>
        /// Makes object fx. Host, User or Course to a Json formated string 
        /// </summary>
        /// <param name="modelOrType"></param>
        /// <returns></returns>
        public static string ConvertObjectToJson(object modelOrType)
        {
            string jsonString = JsonConvert.SerializeObject(modelOrType);
            return jsonString;
        }


        /// <summary>
        /// Takes a Json formated string and converts it to the selected object 
        /// And a string type is host, user or course
        /// </summary>
        /// <param name="jsonString"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object ConvertJsonToObejct(string jsonString, string type)
        {
            var jsonObject = JsonConvert.DeserializeObject(jsonString);
            switch (type)
            {
                case "Host":
                    Host host = JsonConvert.DeserializeObject<Host>(jsonString);
                    return host;

                case "BookingViewModel":
                    BookingViewModel bookingViewModel = JsonConvert.DeserializeObject<BookingViewModel>(jsonString);
                    return bookingViewModel;

                case "User":
                    User user = JsonConvert.DeserializeObject<User>(jsonString);
                    return user;

                case "Course":
                    Course course = JsonConvert.DeserializeObject<Course>(jsonString);
                    return course;
                default:
                    break;
            }
            return null;
        }
    }
}