﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdlånsWeb.Models;

namespace UdlånsWeb.DataHandling
{
    public static class Data
    {
        private static ConvertCourseData convertCourseData { get; set; } = new ConvertCourseData();
        private static ConvertItemData convertItemData { get; set; } = new ConvertItemData();
        private static ConvertUserData convertUserData { get; set; } = new ConvertUserData();
        private static ConvertLoginData convertlogindata { get; set; } = new ConvertLoginData();
        private static ConvertBookingData convertBookingData { get; set; } = new ConvertBookingData();

        public static CourseViewModel CourseViewModel { get; set; }
        public static HostViewModel HostViewModel { get; set; }
        public static UserViewModel UserViewModel { get; set; }
        public static List<BookingViewModel> BookingViewModels { get; set; }
        /// <summary>
        /// Loads data from files to keep while the app is running Will reload/update if runed again
        /// </summary>
        public static void LoadData()
        {
            //Loads all the courses
            CourseViewModel = convertCourseData.GetCourses();
            //Load all the hosts
            HostViewModel = convertItemData.GetItems();
            //Load all the users
            UserViewModel = convertUserData.GetUsers();
            //Load all bookings made
            BookingViewModels = convertBookingData.GetBookings();

        }

        #region Bookings
        public static void SaveBookings(List<BookingViewModel> bookingViewModels)
        {
            convertBookingData.SaveAllBookings(bookingViewModels);
        }
        public static void EditBooking(BookingViewModel bookingViewModel)
        {
            convertBookingData.EditBooking(bookingViewModel);
        }
        public static void DeleteBooking(BookingViewModel bookingViewModel)
        {
            convertBookingData.DeleteBooking(bookingViewModel);
        }
        #endregion
        #region Courses

        public static void SaveCourses(CourseViewModel courseViewModel)
        {
            convertCourseData.SaveAllCourses(courseViewModel);
        }

        public static void EditCourse(Course course)
        {
            convertCourseData.EditCourse(course);
        }

        public static void DeleteCourses(Course course)
        {
            convertCourseData.DeleteCourse(course);
        }
        #endregion
    }
}