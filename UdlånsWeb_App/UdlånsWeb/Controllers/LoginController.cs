using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using UdlånsWeb.DataHandling;
using UdlånsWeb.Models;

namespace UdlånsWeb.Controllers
{
    public class LoginController : Controller
    {
        private static User SelectedUser { get; set; }
        private static Host SelectedItem { get; set; }
        private static BookingViewModel bookingViewModel { get; set; }
        private static BookingViewModel userBooking { get; set; }

        [HttpGet]
        public IActionResult HomePage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult HomePage(string initials)
        {
            Data.GetUsers();
            CurrentUser.User = Data.GetUsers().Users.Where(x => x.Initials == initials).FirstOrDefault();

            //Login still needs some work
            if (CurrentUser.User == null)
                return Redirect("Home/ErrorPage");

            if (CurrentUser.User.Admin == true)
                return Redirect("/Item/AdminSite");

            else
                return Redirect("Booking/BookingDefine");
        }


    }
}
