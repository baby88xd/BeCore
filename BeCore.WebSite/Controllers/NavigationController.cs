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
    public class NavigationController : Controller
    {
        //private ISys_ButtonsRepository _sysButtons { get; set; }
        private ISys_NavigationsRepository sysNav { get; set; }
        private ISys_ButtonsRepository sysbtn { get; set; }

        private ISys_NavButtonsRepository NavBtns { get; set; }
        public NavigationController(ISys_NavigationsRepository sys_Navigations, ISys_ButtonsRepository sys_Buttons, ISys_NavButtonsRepository sys_NavBtns)
        {
            sysNav = sys_Navigations;
            sysbtn = sys_Buttons;
            NavBtns = sys_NavBtns;
        }
        #region 菜单管理
        public IActionResult List()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add(int id = 0)
        {
            Sys_Navigations sys_Navigations = new Sys_Navigations();
            if (id != 0)
            {
                sys_Navigations = sysNav.List(p => p.Id == id).FirstOrDefault();
            }
            return View(sys_Navigations);
        }
        [HttpPost]
        public IActionResult Add(Sys_Navigations model)
        {
            if (model.Id == 0)
                sysNav.Add(model);
            else
                sysNav.Update(model);
            sysNav.Commit();
            return Json("");
        }


        public IActionResult Del(int id)
        {
            sysNav.DeleteWhere(x => x.Id == id, true);
            return Json("");
        }

        public IActionResult NavTree()
        {
            var data = sysNav.GetTrees("");
            return Json(data);
        }

        public IActionResult Data()
        {
            var _all = sysNav.List(x => x.Deleted == false).ToList();
            var ReturnData = new NavModel()
            {
                tip = "操作成功",
                @is = true,
                count = _all.Count,
                code = 0,
                msg = "",
                data = _all

            };
            return Json(ReturnData);
        }
        #endregion

        #region 菜单和按钮
        public IActionResult Btn()
        {
            return View();
        }

        public IActionResult GetMenuTree()
        {

            return Json(sysNav.GetDtree());
        }

        public IActionResult GetBtnTree()
        {
            return Json(sysbtn.GetDtree());
        }

        public IActionResult GetBtn(SearchModel model)
        {
            var WhereLam = model.WhereLambda;
            //Navids
            var _sel = WhereLam.Where(p => p.Key == "Navids").FirstOrDefault();
            if (_sel != null)
            {
                var NavId = int.Parse(_sel.Value.ToString());
                //获取值
                var BtnIds = NavBtns.List(p => p.Deleted == false && p.NavId == NavId).Select(z => z.ButtonId).ToList();
                WhereLam.Remove(_sel);
                model.WhereLambda = WhereLam;
                //WhereLam.Add(new UosoConditions()
                //{
                //    Key = "Id",
                //    Value = BtnIds,
                //    OperatorMethod = "In"
                //});
                return Json(sysbtn.GetPaper(model, p => BtnIds.Contains(p.Id)));
            }
            else
            {
                return Json(sysbtn.GetPaper(model));

            }
            //model.WhereLambda = WhereLam;
        }
        [HttpPost]
        public IActionResult SaveBtn(int menuid, string btnids)
        {
            var Selids = btnids.Split(',').Select(z => new Sys_NavButtons()
            {
                Deleted = false,
                CreateTime = DateTime.Now,
                ButtonId = int.Parse(z),
                NavId = menuid
            });

            NavBtns.AddRange(Selids, true);
            return Json("");
        }

        public IActionResult BindBtn()
        {
            return Json("");
        }
        #endregion

    }
}