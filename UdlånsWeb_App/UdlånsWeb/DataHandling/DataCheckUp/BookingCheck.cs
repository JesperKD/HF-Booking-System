using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdlånsWeb.Models;

namespace UdlånsWeb.DataHandling.DataCheckUp
{
    public static class BookingCheck
    {
        public static void CheckBooking()
        {
            HostViewModel hostViewModel = Data.GetHosts();
            
            Host host = hostViewModel.Hosts.Where(x => x.Rented == true).FirstOrDefault();
            foreach (var item in hostViewModel.Bookings)
            {
                if (item.Id == host.Id)
                {
                    if (item.TurnInDate == DateTime.Now)
                    {
                        host.Rented = false;
                        Data.DeleteBooking(item);
                    }
                }
            }

        }
    }
}
