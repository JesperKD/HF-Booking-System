using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using UdlånsWeb.DataHandling;
using UdlånsWeb.DataHandling.DataCheckUp;
using UdlånsWeb.Models;

namespace UdlånsWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult HomePage()
        {
            BookingCheck.CheckBooking();
            return View();
        }

        [HttpPost]
        public IActionResult HomePage(User user)
        {
            
            //Checks if the user exist in the file 
            if (Data.UserExist(user))
            {
                //If it does find the user info and set them to current user
                CurrentUser.User = Data.GetUsers().Users.Where(x => x.Initials == user.Initials && x.Password == user.Password).FirstOrDefault();
            }
            else
            {
                //if no user found set current user to null
                CurrentUser.User = null;
            }

            if (CurrentUser.User == null)
                return Redirect("Home/ErrorPage");

            if (CurrentUser.User.Admin == true)
                return Redirect("/Item/AdminSite");

            else
                return Redirect("Booking/BookingDefine");
        }

        public IActionResult Privacy()
        {
            var ItemModel = new HostViewModel();
            ItemModel = Data.GetHosts();
            return View(ItemModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ErrorPage()
        {
            return View(new ErrorViewModel());
        }
    }
}
