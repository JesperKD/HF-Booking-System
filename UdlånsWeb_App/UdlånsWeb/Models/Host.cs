using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using UdlånsWeb.DataHandling;

namespace UdlånsWeb.Models
{
    public class Host : IComparable<Host>, IEquatable<Host>
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
        
       /// <summary>
       /// This method is used for sorting lists of hosts 
       /// so it only matches the id of the host
       /// </summary>
       /// <param name="other"></param>
       /// <returns></returns>
        public int CompareTo(Host other)
        {
            if (other == null)
                return 1;
            else
                return this.Id.CompareTo(other.Id);
        }

        public bool Equals(Host other)
        {
            if (other.Name == this.Name)
                return true;
            else
                return false;
        }
    }
}
