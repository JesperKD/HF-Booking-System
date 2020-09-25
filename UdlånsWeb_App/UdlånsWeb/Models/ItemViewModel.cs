using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace UdlånsWeb.Models
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        public List<Item> Items { get; set; }
        //public List<BookingViewModel> Bookings { get; set; }
        public ItemViewModel()
        {
            Items = new List<Item>();
        }
    }
}
