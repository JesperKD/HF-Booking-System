using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using UdlånsWeb.DataHandling;

namespace UdlånsWeb.Models
{
    public class Item
    {
        [DisplayName("Host Navn")]
        public string HostName { get; set; }
        [DisplayName("Adgangskode")]
        public string HostPassword { get; set; }
        [DisplayName("Brugernavn")]
        public string UserName { get; set; }
        [DisplayName("vmWare Version")]
        public string VmWareVersion { get; set; }
        [DisplayName("Host IP")]
        public string HostIp { get; set; }        
        [DisplayName("Lånes fra")]
        public DateTime RentedDate { get; set; }
        [DisplayName("Lånes til")]
        public DateTime TurnInDate { get; set; }
        [DisplayName("Udlånt")]
        public bool Rented { get; set; }
        public int Id { get; set; }
        public bool InUse { get; set; }
        [DisplayName("Beskrivelse")]
        public string Description { get; set; }
    }
}
