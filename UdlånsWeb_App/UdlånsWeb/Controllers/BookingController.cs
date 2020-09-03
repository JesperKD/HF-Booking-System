using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text;
using UdlånsWeb.DataHandling;
using UdlånsWeb.Models;

namespace UdlånsWeb.Controllers
{
    public class BookingController : Controller
    {
        private static BookingViewModel bookingViewModel { get; set; }
        private static BookingViewModel userBooking { get; set; }
        ConvertUserData ConvertUserData = new ConvertUserData();

        [HttpGet]
        public IActionResult BookingDefine()
        {
            if (CurrentUser.User == null)
                return Redirect("Home/ErrorPage");

            return View();
        }
        [HttpGet]
        public IActionResult InfoPage(Course course)
        {
            if (CurrentUser.User == null)
                return Redirect("Home/ErrorPage");

            if (bookingViewModel == null)
            {
                bookingViewModel = new BookingViewModel
                {
                    CourseModel = new Course()
                    {
                        Name = course.Name,
                        CourseNumber = course.CourseNumber,
                        Defined = course.Defined
                    },

                    RentDate = DateTime.Now.Date,
                };
            }

            bookingViewModel.CoursesForSelection = Data.GetCourses().Courses;
            bookingViewModel.CurrentUser = CurrentUser.User;
            return View(bookingViewModel);
        }

        [HttpPost]
        public IActionResult InfoPage(BookingViewModel booking)
        {
            //Finds the course matching the chosen course name
            Course course = Data.GetCourses().Courses.Where(x => x.Id == booking.CourseModel.Id).FirstOrDefault();
            //Sets the course defined to user choise
            course.Defined = booking.CourseModel.Defined;
            //Sets the number of students for the selected course
            course.NumberOfStudents = booking.CourseModel.NumberOfStudents;
            //Puts the user defined data to the booking view model
            booking.CourseModel = course;
            //Adds the booking to userBooking for save later
            userBooking = booking;
            return Redirect("ConfirmBooking");
        }

        public IActionResult ConfirmBooking(BookingViewModel booking)
        {
            int availableSpotsForStudents = userBooking.CourseModel.NumberOfStudents;
            var hostViewModel = Data.GetHosts();
            foreach (var item in hostViewModel.Hosts)
            {
                //Makes sure that there is enough hosts for the class
                if (item.NumberOfPeoplePerHost >= availableSpotsForStudents && item.Rented == false)
                {
                    if (userBooking.CourseModel.Defined == false)
                    {
                        item.Rented = true;
                        userBooking.TurnInDate = userBooking.RentDate.AddDays(userBooking.CourseModel.Duration);
                        booking.HostsRentedForCourse.Add(item);
                    }
                    availableSpotsForStudents += item.NumberOfPeoplePerHost - availableSpotsForStudents;
                    userBooking.CurrentUser = CurrentUser.User;
                }
            }
            Data.HostData.Bookings.Add(userBooking);
            return View(userBooking);
        }

        public IActionResult BookingSucces()
        {
            Data.SaveHosts();

            // send mail to user and admins
            UserViewModel users = ConvertUserData.GetUsers();
            UserViewModel mailRecipients = new UserViewModel();
            foreach (User user in users.Users)
            {
                if (user.Email == CurrentUser.User.Email && user.Initials == CurrentUser.User.Initials)
                {
                    mailRecipients.Users.Add(user);
                    continue;
                }
                if (user.Admin == true)
                {
                    mailRecipients.Users.Add(user);
                    continue;
                }
            }
            // New stringbuilder
            StringBuilder stringBuilder = new StringBuilder();

            // String for email
            stringBuilder.Append("Booking summary:" + Environment.NewLine + "Lærer initialer: " + CurrentUser.User.Initials
                + Environment.NewLine + "Fag: " + userBooking.CourseModel.Name + Environment.NewLine
                + "Booket fra " + userBooking.HostsRentedForCourse.First().RentedDate + " - til " + userBooking.HostsRentedForCourse.First().TurnInDate
                + Environment.NewLine + "Hostnavn       Ip      User        Password" + Environment.NewLine
                );

            foreach (var item in userBooking.HostsRentedForCourse)
            {
                stringBuilder.Append(item.Name + "       " + item.HostIp + "       " + item.UserName + "        " + item.Password + Environment.NewLine);
            }

            stringBuilder.Append("Antal grupper pr host [" + userBooking.CourseModel.NumberOfStudents + "]" + Environment.NewLine
                + Environment.NewLine + "Adgang til serveren kan etableres via følgnde netværk:"
                + "Trådløst (Når eleverne er på skolen) - Forbind til DataExpNet" + Environment.NewLine
                + "(kode: Just@Salt&Vinegar666)" + Environment.NewLine
                + "VPN (Når eleverne er hjemme) - Følg vejledningen til installation" + Environment.NewLine
                + "af VPN forbindelsen og forbind hrefter med jeres ZBC initialer" + Environment.NewLine
                + "(Kode: Just@Salt&Vinegar666)" + Environment.NewLine + Environment.NewLine
                + "!!! Husk at bede dine elever om at ryde op på hostn inden" + Environment.NewLine
                + "faget slutter !!!");

            MailSending.Email(stringBuilder.ToString(), mailRecipients);

            return View(CurrentUser.User);
        }

        public IActionResult Bookings()
        {
            if (Data.GetHosts().Bookings.Count != 0 || Data.GetHosts().Bookings != null)
            {
                Data.GetHosts();
                return View(Data.HostData.Bookings);
            }
            else
            {
                return View(new BookingViewModel());
            }
        }

    }
}
