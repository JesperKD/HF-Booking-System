using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
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
        Data Data = new Data();
        private static User SelectedUser { get; set; }
        private static Host SelectedItem { get; set; }
        private static BookingViewModel bookingViewModel { get; set; }
        private static BookingViewModel userBooking { get; set; }

        [HttpGet]
        public IActionResult AdminSite()
        {
            if (CurrentUser.User == null || CurrentUser.User.Admin == false)
                return Redirect("ErrorPage");

            HostViewModel itemModel = Data.ConvertItemData.GetItems();
            try
            {
                if (Data.ConvertBookingData.GetBookings() != null || Data.ConvertBookingData.GetBookings().Count == 0) 
                    itemModel.Bookings = Data.ConvertBookingData.GetBookings();

                if (itemModel == null)
                {
                    itemModel = new HostViewModel();
                }
                foreach (var item in itemModel.Items)
                {
                    //if host is rented 
                    if (item.Rented == true)
                    {
                        //check bookings for a turn in date 
                        foreach (var booking in itemModel.Bookings)
                        {
                            //checks if turn in date has run out and if the booking id macth the item/host id
                            if (booking.HostRentedForCourse.TurnInDate == DateTime.Now.Date && booking.Id == item.Id)
                            {
                                //reset rented so the view model updates
                                //Used by admin to show if host is used or not
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
            Data.ConvertItemData.AddItem(item);
            return Redirect("AdminSite");
        }

        // item is null
        [HttpGet]
        public IActionResult EditItem(HostViewModel item, int id)
        {
            if (CurrentUser.User == null && CurrentUser.User.Admin == true)
                return Redirect("Home/ErrorPage");

            return View(item.Items[id]);
        }

        [HttpPost]
        public IActionResult EditItem(Host item)
        {
            //Checks after a booking on the host and delete it aswell
            List<BookingViewModel> bookings = Data.ConvertBookingData.GetBookings();
            foreach (var booking in bookings)
            {
                if (item.Id == booking.Id)
                {
                    Data.ConvertBookingData.DeleteBooking(booking);
                }
            }
            Data.ConvertItemData.EditItem(item);
            return Redirect("AdminSite");
        }

        [HttpGet]
        public IActionResult DeleteItem(HostViewModel item, int id)
        {
            if (CurrentUser.User == null && CurrentUser.User.Admin == true)
                return Redirect("Home/ErrorPage");

            SelectedItem = item.Items[id];
            return View(SelectedItem);
        }

        [HttpPost]
        public IActionResult DeleteItem(Host item)
        {
            Data.ConvertItemData.DeleteItem(item);
            //Checks after a booking on the host and delete it aswell
            List<BookingViewModel> bookings = Data.ConvertBookingData.GetBookings();
            foreach (var booking in bookings)
            {
                if (item.Id == booking.Id) 
                {
                    Data.ConvertBookingData.DeleteBooking(booking);
                }
            }
            return Redirect("AdminSite");
        }

    }
}
