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
        public static List<Item> GetItems()
        {
            List<Item> items = new List<Item>() {
                new Item(){HostName = "Host1", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Item(){HostName = "Host2", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.10.0", HostIp = "110.0.0.1", Rented = false },
                new Item(){HostName = "Host3", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true },
                new Item(){HostName = "Host4", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Item(){HostName = "Host5", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Item(){HostName = "Host6", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Item(){HostName = "Host7", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Item(){HostName = "Host8", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Item(){HostName = "Host9", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = false},
                new Item(){HostName = "Host10", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = false},
                new Item(){HostName = "Host11", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Item(){HostName = "Host12", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = false},
                new Item(){HostName = "Host13", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Item(){HostName = "Host14", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Item(){HostName = "Host15", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = false},
                new Item(){HostName = "Host16", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Item(){HostName = "Host17", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Item(){HostName = "Host18", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = false},
                new Item(){HostName = "Host18", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = false},
                new Item(){HostName = "Host19", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Item(){HostName = "Host20", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Item(){HostName = "Host21", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = false},
                new Item(){HostName = "Host22", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Item(){HostName = "Host23", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Item(){HostName = "Host24", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Item(){HostName = "Host25", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = false},
                new Item(){HostName = "Host26", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Item(){HostName = "Host27", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Item(){HostName = "Host28", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = false},
                new Item(){HostName = "Host29", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = false},
                new Item(){HostName = "Host30", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = false},
                new Item(){HostName = "Host31", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Item(){HostName = "Host32", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Item(){HostName = "Host33", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Item(){HostName = "Host34", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = false},
                new Item(){HostName = "Host35", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Item(){HostName = "Host36", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Item(){HostName = "Host37", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Item(){HostName = "Host38", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = false},
                new Item(){HostName = "Host39", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = false},
                new Item(){HostName = "Host40", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = false},
                new Item(){HostName = "Host41", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Item(){HostName = "Host42", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Item(){HostName = "Host43", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Item(){HostName = "Host44", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = false},
                new Item(){HostName = "Host45", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = true},
                new Item(){HostName = "Host46", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = false},
                new Item(){HostName = "Host47", HostPassword = "kode1234!", UserName = "root", VmWareVersion = "2.14.1", HostIp = "101.0.0.1", Rented = false}
            };
            return items;
        }

        public static List<User> GetUsers()
        {
            List<User> users = new List<User>() { 
                new User(){Name = "Flemming", Initials = "FlemAK", Email = "Flemse@ZBC.dk", Admin = true }, 
                new User(){Name = "Janni", Initials = "JanniS", Email = "JanniH@ZBC.dk", Admin = true}, 
            };

            return users;
        } 
    }
}
