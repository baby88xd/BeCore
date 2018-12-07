using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeCore.WebSite.Models;
using BeCore.Core.Interfaces;
using System.Text;
namespace BeCore.WebSite.Controllers
{
    public class HomeController : Controller
    {
        private ISys_NavigationsRepository sysNav { get; set; }
        //public NavigationController(ISys_NavigationsRepository sys_Navigations)

        public HomeController(ISys_NavigationsRepository sys_Navigations)
        {
            sysNav = sys_Navigations;
        }

        public IActionResult Index()
        {
            //StringBuilder _sb = new StringBuilder();
            ViewBag.Nav = sysNav.GetNavHtml();
            return View();
        }

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
