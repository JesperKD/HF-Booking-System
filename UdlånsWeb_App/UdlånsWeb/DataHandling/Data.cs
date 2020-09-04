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
        private static MailSending mailsending { get; set; } = new MailSending();
        

        public static CourseViewModel CourseData { get; set; }
        public static HostViewModel HostData { get; set; }
        public static UserViewModel UserData { get; set; }
        public static List<BookingViewModel> BookingData { get; set; }
        #region Booking
        public static void DeleteBooking (BookingViewModel bookingViewModel)
        {
            Data.HostData.Bookings = Data.GetHosts().Bookings;

            BookingViewModel bookingToDelete = Data.HostData.Bookings.Where(x => x.Id == bookingViewModel.Id).FirstOrDefault();
            Data.HostData.Bookings.Remove(bookingToDelete);
            Data.SaveHosts();
        }

        public static void EditBooking(BookingViewModel bookingViewModel)
        {
            Data.HostData.Bookings = Data.GetHosts().Bookings;

            BookingViewModel bookingToDelete = Data.HostData.Bookings.Where(x => x.Id == bookingViewModel.Id).FirstOrDefault();
            Data.HostData.Bookings.Remove(bookingToDelete);
            Data.HostData.Bookings.Add(bookingViewModel);
            Data.SaveHosts();
        }

        #endregion
        #region Host
        public static HostViewModel GetHosts()
        {          
            HostData = convertHostData.GetHosts();      
            return HostData;
        }

        /// <summary>
        /// Make sure to add the host to Data.HostViewModel.Hosts
        /// </summary>
        public static void SaveHosts()
        {
            convertHostData.ReWriteHostFile(HostData);
        }
        /// <summary>
        /// Deletes the host from the file
        /// </summary>
        /// <param name="host"></param>
        public static void DeleteHost(Host host)
        {
            //Get the hosts from the file
            HostData = convertHostData.GetHosts();
            //Finds the user getting deleted
            Host hostGettingDeleted = HostData.Hosts.Where(x => x.Id == host.Id).FirstOrDefault();

            foreach (var booking in HostData.Bookings)
            {
                if(booking.Id == hostGettingDeleted.Id)
                {
                    Data.DeleteBooking(booking);
                }
            }

            //Deletes the old host data from the file
            HostData.Hosts.Remove(hostGettingDeleted);
            //Overrides the file with all the hosts                    
            convertHostData.ReWriteHostFile(HostData);
        }
        public static void EditHost(Host host)
        {
            //Get the hosts from the file
            HostData = convertHostData.GetHosts();
            //Finds the user getting edited
            Host hostGettingDeleted = HostData.Hosts.Where(x => x.Id == host.Id).FirstOrDefault();
            //Deletes the old host data from the file
            HostData.Hosts.Remove(hostGettingDeleted);
            //Adds the new edited host to the host view model
            HostData.Hosts.Add(host);
            //Overrides the file with all the hosts
            convertHostData.ReWriteHostFile(HostData);
        }
        #endregion

        #region User
        /// <summary>
        /// Gets the users from file then save them in UserViewModel and returns a UserViewModel 
        /// </summary>
        /// <returns></returns>
        public static UserViewModel GetUsers()
        {
            if (convertUserData.GetUsers() != null)
            {
                UserData = convertUserData.GetUsers();
            }
            else
            {
                UserData = new UserViewModel();
            }
            return UserData;
        }

        public static bool UserExist(User user)
        {
            return convertUserData.DoesUserExist(user);
        }

        /// <summary>
        /// Saves the users in UserViewModel
        /// </summary>
        public static void SaveUsers()
        {
            convertUserData.ReWriteUserFile(UserData);
        }
        public static void DeleteUser(User user)
        {
            GetUsers();
            User userToDelete = UserData.Users.Where(x => x.Id == user.Id).FirstOrDefault();
            UserData.Users.Remove(userToDelete);
            SaveUsers();
        }
        public static void EditUser(User user)
        {
            GetUsers();
            User userToDelete = UserData.Users.Where(x => x.Initials == user.Initials && x.Password == user.Password).FirstOrDefault();
            UserData.Users.Remove(userToDelete);
            UserData.Users.Add(user);
            SaveUsers();
        }
        #endregion


        #region Course
        public static CourseViewModel GetCourses()
        {
            if (convertCourseData.GetCourses().Courses.Count != 0)
            {
                CourseData = convertCourseData.GetCourses();
            }
            else
            {
                CourseData = new CourseViewModel();
            }
            return CourseData;
        }
        /// <summary>
        /// Saves the users in UserViewModel
        /// </summary>
        public static void SaveCourses()
        {
            convertCourseData.RewriteCourseFile(CourseData);
        }
        public static void DeleteCourse(Course course)
        {
            GetCourses();

            Course courseToDelete = CourseData.Courses.Where(x => x.Id == course.Id).FirstOrDefault();
            CourseData.Courses.Remove(courseToDelete);
            SaveCourses();
        }
        public static void EditCourse(Course course)
        {
            GetUsers();
            Course courseToDelete = CourseData.Courses.Where(x => x.Id == course.Id).FirstOrDefault();
            CourseData.Courses.Remove(courseToDelete);
            CourseData.Courses.Add(course);
            SaveCourses();
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
            switch (type)
            {
                case "HostViewModel":
                    HostViewModel hostViewModel = JsonConvert.DeserializeObject<HostViewModel>(jsonString);
                    return hostViewModel;

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