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
        
        private static User SelectedUser { get; set; }
        private static Host SelectedItem { get; set; }
        private static BookingViewModel bookingViewModel { get; set; }
        private static BookingViewModel userBooking { get; set; }

        [HttpGet]
        public IActionResult AdminSite()
        {
            if (CurrentUser.User == null || CurrentUser.User.Admin == false)
                return Redirect("ErrorPage");

            HostViewModel itemModel = Data.HostViewModel;
            try
            {
                if (Data.BookingViewModels != null || Data.BookingViewModels.Count == 0) 
                    itemModel.Bookings = Data.BookingViewModels;

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
            if (CurrentUser.User == null && CurrentUser.User.Admin == true)
                return Redirect("Home/ErrorPage");

            return View();
        }

        [HttpPost]
        public IActionResult AddItem(Host item)
        {
            
            return Redirect("AdminSite");
        }

        // item is null
        [HttpGet]
        public IActionResult EditItem(HostViewModel item, int id)
        {
            if (CurrentUser.User == null && CurrentUser.User.Admin == true)
                return Redirect("Home/ErrorPage");

            return View(item.Hosts[id]);
        }

        [HttpPost]
        public IActionResult EditItem(Host item)
        {
            //Checks after a booking on the host and delete it aswell
            Data.BookingViewModels = Data.;
            foreach (var booking in bookings)
            {
                if (item.Id == booking.Id)
                {
                    
                }
            }
          
            return Redirect("AdminSite");
        }

        [HttpGet]
        public IActionResult DeleteItem(HostViewModel item, int id)
        {
            if (CurrentUser.User == null && CurrentUser.User.Admin == true)
                return Redirect("Home/ErrorPage");

            SelectedItem = item.Hosts[id];
            return View(SelectedItem);
        }

        [HttpPost]
        public IActionResult DeleteItem(Host host)
        {
            Data.DeleteHost(host);
            return Redirect("AdminSite");
        }

    }
}
