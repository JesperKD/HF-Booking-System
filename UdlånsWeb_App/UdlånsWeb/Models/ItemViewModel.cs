using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdlånsWeb.Models
{
    public class ItemViewModel
    {
        public List<Item> Items { get; set; }
        public ItemViewModel()
        {
            Items = new List<Item>();
        }
    }
}
