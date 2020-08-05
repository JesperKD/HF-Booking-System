using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdlånsWeb.DataHandling;

namespace UdlånsWeb.Models
{
    public class ItemModel
    {
        public string Rack { get; set; }
        public string Ip { get; set; }
        public string HostName { get; set; }
        public string HostPassword { get; set; }
        public ItemModel()
        {

        }
    }
}
