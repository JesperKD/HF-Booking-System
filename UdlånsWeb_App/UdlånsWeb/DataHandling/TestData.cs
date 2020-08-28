using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using UdlånsWeb.Models;

namespace UdlånsWeb.DataHandling
{
    public static class TestData
    {
        public static List<Host> GetItems()
        {
            List<Host> items = new List<Host>() {
                new Host(){Name = "Host1", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Host(){Name = "Host2", Password = "kode1234!", UserName = "root", VmWareVersion = "2.10.0", HostIp = "110.0.0.1", Rented = false },
                new Host(){Name = "Host3", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true },
                new Host(){Name = "Host4", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Host(){Name = "Host5", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Host(){Name = "Host6", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Host(){Name = "Host7", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Host(){Name = "Host8", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Host(){Name = "Host9", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = false},
                new Host(){Name = "Host10", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = false},
                new Host(){Name = "Host11", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Host(){Name = "Host12", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = false},
                new Host(){Name = "Host13", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Host(){Name = "Host14", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Host(){Name = "Host15", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = false},
                new Host(){Name = "Host16", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Host(){Name = "Host17", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Host(){Name = "Host18", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = false},
                new Host(){Name = "Host18", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = false},
                new Host(){Name = "Host19", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Host(){Name = "Host20", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Host(){Name = "Host21", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = false},
                new Host(){Name = "Host22", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Host(){Name = "Host23", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Host(){Name = "Host24", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Host(){Name = "Host25", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = false},
                new Host(){Name = "Host26", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Host(){Name = "Host27", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Host(){Name = "Host28", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = false},
                new Host(){Name = "Host29", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = false},
                new Host(){Name = "Host30", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = false},
                new Host(){Name = "Host31", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Host(){Name = "Host32", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Host(){Name = "Host33", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Host(){Name = "Host34", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = false},
                new Host(){Name = "Host35", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Host(){Name = "Host36", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Host(){Name = "Host37", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Host(){Name = "Host38", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = false},
                new Host(){Name = "Host39", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = false},
                new Host(){Name = "Host40", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = false},
                new Host(){Name = "Host41", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Host(){Name = "Host42", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Host(){Name = "Host43", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Host(){Name = "Host44", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = false},
                new Host(){Name = "Host45", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Host(){Name = "Host46", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = false},
                new Host(){Name = "Host47", Password = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = false}
            };
            return items;
        }

        public static List<User> GetUsers()
        {
            List<User> users = new List<User>() { 
                new User(){FirstName = "Flemming", Initials = "FlemAK", Email = "Flemse@ZBC.dk", Admin = true }, 
                new User(){FirstName = "Janni", Initials = "JanniS", Email = "JanniH@ZBC.dk", Admin = true}, 
            };

            return users;
        } 
    }
}
