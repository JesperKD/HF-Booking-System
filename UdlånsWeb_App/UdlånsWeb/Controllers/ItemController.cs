using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using UdlånsWeb.DataHandling;
using UdlånsWeb.DataHandling.DataCheckUp;
using UdlånsWeb.Models;


namespace UdlånsWeb.Controllers
{
    public class ItemController : Controller
    {
        [HttpGet]
        public IActionResult AdminSite()
        {
            if (CurrentUser.User == null || CurrentUser.User.Admin == false)
                return Redirect("ErrorPage");
       
            BookingCheck.CheckBooking();
            Data.GetHosts();
            Data.HostData.Bookings = Data.GetHosts().Bookings;
            Data.HostData.Hosts.Sort();
            return View(Data.HostData);
        }

        [HttpGet]
        public IActionResult AddItem()
        {
            if (CurrentUser.User == null || CurrentUser.User.Admin == false)
                return Redirect("Home/ErrorPage");

            return View();
        }

        [HttpPost]
        public IActionResult AddItem(Host host)
        {
            IdControl.GiveIdToHost(host);
            Data.HostData.Hosts.Add(host);
            Data.SaveHosts();
            return Redirect("AdminSite");
        }

        // item is null
        [HttpGet]
        public IActionResult EditItem(int id)
        {
            if (CurrentUser.User == null || CurrentUser.User.Admin == false)
                return Redirect("Home/ErrorPage");

            Host hostToEdit = Data.HostData.Hosts.Where(x => x.Id == id).FirstOrDefault();
            return View(hostToEdit);
        }

        [HttpPost]
        public IActionResult EditItem(Host host)
        {
            //Checks after a booking on the host and delete it aswell
            List<BookingViewModel> bookingViewModel = Data.GetHosts().Bookings;
            foreach (var booking in bookingViewModel)
            {
                if (host.Id == booking.Id)
                {
                    Data.DeleteBooking(booking);
                }
            }
            Data.EditHost(host);
            return Redirect("AdminSite");
        }

        [HttpGet]
        public IActionResult DeleteItem(int id)
        {
            if (CurrentUser.User == null || CurrentUser.User.Admin == false)
                return Redirect("Home/ErrorPage");

            Host hostToDelete = Data.HostData.Hosts.Where(x => x.Id == id).FirstOrDefault();
            return View(hostToDelete);
        }

        [HttpPost]
        public IActionResult DeleteItem(Host host)
        {
            List<BookingViewModel> bookingViewModel = Data.GetHosts().Bookings;
            foreach (var booking in bookingViewModel)
            {
                if (host.Id == booking.Id)
                {
                    Data.DeleteBooking(booking);
                }
            }
            Data.DeleteHost(host);
            return Redirect("AdminSite");
        }
    }
}
