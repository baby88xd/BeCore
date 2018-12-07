using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BeCore.Core.Interfaces;
using BeCore.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace BeCore.WebSite.Controllers
{
    public class ButtonController : Controller
    {
        private ISys_ButtonsRepository _sysButtons { get; set; }
        public ButtonController(ISys_ButtonsRepository sysButtons)
        {
            _sysButtons = sysButtons;
        }

        /// <summary>
        /// 展示页面
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }



        #region 添加按钮
        [HttpGet]
        public IActionResult Add(int id = 0)
        {
            var model = new Sys_Buttons();
            if (id != 0)
                model = _sysButtons.List(x => x.Id == id).FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(Sys_Buttons model)
        {
            if (model.Id == 0)
            {
                _sysButtons.Add(model);
            }
            else
            {
                _sysButtons.Update(model);
            }
            _sysButtons.Commit();
            return Json("");
        }
        #endregion

        public IActionResult Del(int id)
        {
            _sysButtons.DeleteWhere(x => x.Id == id, true);
            return Json("");

        }


        #region 获取列表数据
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IActionResult Data(SearchModel model)
        {
            return Json(_sysButtons.GetPaper(model));
        }
        #endregion
    }
}