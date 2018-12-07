using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeCore.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BeCore.WebSite.Controllers
{
    public class DataController : Controller
    {
        private ISys_NavigationsRepository sysNav { get; set; }
        private ISys_ButtonsRepository sysbtn { get; set; }
        public DataController(ISys_ButtonsRepository sys_Buttons, ISys_NavigationsRepository _sysNav)
        {
            sysNav = _sysNav;
            sysbtn = sys_Buttons;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult RoleNav(int id = 0)
        {
            return Json(sysNav.GetDtreeBtn(id));
        }

    }
}