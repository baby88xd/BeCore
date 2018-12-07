using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BeCore.WebSite.Controllers
{
    public class specialController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}