using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UdlånsWeb.Controllers
{
    public class UserController : Controller
    {
       
        // add all methods with User tag
        public IActionResult Index()
        {
            return View();
        }
    }
}
