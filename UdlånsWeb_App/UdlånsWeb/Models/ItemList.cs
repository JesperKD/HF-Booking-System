using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdlånsWeb.Models
{
    public class ItemList
    {
        public List<ItemModel> Items { get; set; }
        public ItemList()
        {
            Items = new List<ItemModel>();
        }
    }
}
