using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdlånsWeb.DataHandling
{
    public static class LocationData
    {
        public static List<Location> Locations { get; set; }

        public static List<Location> GetLocations()
        {
            Locations = new List<Location>() { new Location(1, 'A'), new Location(2, 'A'), new Location(3, 'A'), new Location(4, 'A') };
            return Locations;
        }
    }
}
