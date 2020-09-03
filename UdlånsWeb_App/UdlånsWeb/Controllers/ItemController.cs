﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using UdlånsWeb.DataHandling;
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

            Data.GetHosts();
            Data.HostData.Bookings = Data.GetBookings();

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
            Data.HostData.Hosts.Add(host);
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
            Data.EditHost(host);
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
