using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using UdlånsWeb.DataHandling;

namespace UdlånsWeb.Models
{
    public class Host
    {
        [DisplayName("Host Navn")]
        public string Name { get; set; }
        [DisplayName("Adgangskode")]
        public string Password { get; set; }
        [DisplayName("Brugernavn")]
        public string UserName { get; set; }
        public string VmWareVersion { get; set; }
        [DisplayName("Host IP")]
        public string HostIp { get; set; }
        [DisplayName("Max antal elever")]
        public int NumberOfPeoplePerHost { get; set; }
        [DisplayName("Lånes fra")]
        public DateTime RentedDate { get; set; }
        [DisplayName("Lånes til")]
        public DateTime TurnInDate { get; set; }
        public bool Rented { get; set; }
        public int Id { get; set; }

    }
}
