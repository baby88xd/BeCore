using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BaseCore.Utility.HelperModel;
using BeCore.Core.Interfaces;
using BeCore.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace BeCore.WebSite.Controllers
{
    public class RoleController : Controller
    {

        private ISys_RolesRepository sys_Roles;
        public RoleController(ISys_RolesRepository sys_RolesRepository)
        {
            sys_Roles = sys_RolesRepository;
        }

        #region 用户角色
        /// <summary>
        /// 用户角色页面
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add(int id = 0)
        {
            Sys_Roles _model = new Sys_Roles();
            if (id != 0)
            {
                _model = sys_Roles.List(c => c.Id == id).FirstOrDefault();
            }
            return View(_model);
        }
        [HttpPost]
        public IActionResult Add(Sys_Roles model)
        {
            if (model.Id == 0)
            {
                //添加
                sys_Roles.Add(model);
            }
            else
            {
                //修改
                sys_Roles.Update(model);
            }
            sys_Roles.Commit();
            return Json(new ResponseModel() { Status = "ok" });
            //\
        }


        public IActionResult del(int id)
        {
            sys_Roles.DeleteWhere(x => x.Id == id, true);
            return Json(new ResponseModel() { Status = "ok" });
        }

        public IActionResult Data(SearchModel model)
        {
            return Json(sys_Roles.GetPaper(model));
        }
        #endregion
    }
}