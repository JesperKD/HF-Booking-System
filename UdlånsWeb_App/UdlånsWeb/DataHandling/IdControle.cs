using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdlånsWeb.Models;

namespace UdlånsWeb.DataHandling
{
    public static class IdControl
    {
        /// <summary>
        /// Gives the Host an id
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public static Host GiveIdToHost(Host host)
        {
            host.Id = GetUnUsedIdForHost();
            return host;
        }
        /// <summary>
        /// Checks if the host id already exist
        /// </summary>
        /// <returns></returns>
        public static int GetUnUsedIdForHost()
        {
            var hostmodel = Data.GetHosts().Hosts;
            if (hostmodel.Count != 0)
            {
                hostmodel.Sort();
                int highestIdNumber = hostmodel.Last().Id;
                return highestIdNumber + 1;
            }
            return 0;
        }
    }
}
