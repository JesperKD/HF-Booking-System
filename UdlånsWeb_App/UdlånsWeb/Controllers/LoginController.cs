using Microsoft.AspNetCore.Mvc;
using UdlånsWeb.Models;

namespace UdlånsWeb.Controllers
{
    public class LoginController : Controller
    {
        private static User SelectedUser { get; set; }
        private static Host SelectedItem { get; set; }
        private static BookingViewModel bookingViewModel { get; set; }
        private static BookingViewModel userBooking { get; set; }

        public IActionResult Index()
        {
            return View();
        }



    }
}
