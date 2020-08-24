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
        [DisplayName ("Host Navn")]
        public string HostName { get; set; }
        [DisplayName ("Adgangskode")]
        public string HostPassword { get; set; }
        [DisplayName ("Brugernavn")]
        public string UserName { get; set; }
        public string VmWareVersion { get; set; }
        public string HostIp { get; set; }
        [DisplayName ("Antal enheder per host")]
        public int NumberOfPeoplePerHost { get; set; }
        [DisplayName("Udlejnings dato")]
        public DateTime RentedDate { get; set; }
        public bool Rented { get; set; }
        public int Id { get; set; }

    }
}
