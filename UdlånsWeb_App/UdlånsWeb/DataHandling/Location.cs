using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdlånsWeb.DataHandling
{
    public class Location
    {
        public int Number { get; set; }
        public char Letter { get; set; }
        public string StorageLocation { get; set; }
        public bool Occupied { get; set; }

        public Location(int number, char letter)
        {
            Number = number;
            Letter = letter;
            StorageLocation = Letter + Number.ToString();
        }
    }
}
