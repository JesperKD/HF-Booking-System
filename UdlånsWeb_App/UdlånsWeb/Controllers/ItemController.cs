using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using UdlånsWeb.DataHandling;
using UdlånsWeb.Models;


namespace UdlånsWeb.Controllers
{
    public class ItemController : Controller
    {
        
        
        private static Host SelectedItem { get; set; }

        [HttpGet]
        public IActionResult AdminSite()
        {
            if (CurrentUser.User == null || CurrentUser.User.Admin == false)
                return Redirect("ErrorPage");
            
            HostViewModel itemModel = Data.GetHosts();
            try
            {
                if (itemModel.Bookings != null || Data.BookingViewModels.Count != 0) 
                    itemModel.Bookings = Data.GetBookings();

                if (itemModel == null)
                {
                    itemModel = new HostViewModel();
                }
                foreach (var item in itemModel.Hosts)
                {
                    //if host is rented 
                    if (item.Rented == true)
                    {
                        foreach (var booking in itemModel.Bookings)
                        {
                            if(booking.Id == item.Id && booking.TurnInDate >= DateTime.Now)
                            {
                                item.Rented = false;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return View(new HostViewModel());
            }
            return View(itemModel);
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
            Data.HostViewModel.Hosts.Add(host);
            Data.SaveHosts();
            return Redirect("AdminSite");
        }

        // item is null
        [HttpGet]
        public IActionResult EditItem(HostViewModel item, int id)
        {
            if (CurrentUser.User == null || CurrentUser.User.Admin == false)
                return Redirect("Home/ErrorPage");

            return View(item.Hosts[id]);
        }

        [HttpPost]
        public IActionResult EditItem(Host host)
        {
            //Checks after a booking on the host and delete it aswell
            List<BookingViewModel> bookingViewModel = Data.GetBookings();
            foreach (var booking in bookingViewModel)
            {
                if (host.Id == booking.Id)
                {
                    Data.DeleteBooking(booking);
                }
            }
            Data.HostViewModel.Hosts.Add(host);
            return Redirect("AdminSite");
        }

        [HttpGet]
        public IActionResult DeleteItem(HostViewModel item, int id)
        {
            if (CurrentUser.User == null || CurrentUser.User.Admin == false)
                return Redirect("Home/ErrorPage");

            return View(item.Hosts[id]);
        }

        [HttpPost]
        public IActionResult DeleteItem(Host host)
        {
            List<BookingViewModel> bookingViewModel = Data.GetBookings();
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
