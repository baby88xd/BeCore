using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeCore.Core.Interfaces;
using BeCore.Core.Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using BaseCore.Utility.HelperModel;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace BeCore.WebSite.Controllers
{
    public class DepartmentController : Controller
    {

        private ISys_DepartmentsRepository _department { get; set; }
        private ISys_UsersRepository _user { get; set; }

        public DepartmentController(ISys_DepartmentsRepository _DepartmentsRepository, ISys_UsersRepository sys_UsersRepository)
        {
            _department = _DepartmentsRepository;
            _user = sys_UsersRepository;
        }
        /// <summary>
        /// 部门首页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 添加部门
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Add(int id = 0)
        {
            var model = new BeCore.Core.Model.Sys_Departments();
            if (id != 0)
                model = _department.List(x => x.Id == id).FirstOrDefault();
            return View(new BeCore.Core.Model.Sys_Departments());
        }

        /// <summary>
        /// 添加部门数据方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add(Sys_Departments model)
        {
            if (model.Id == 0)//添加
                _department.Add(model);
            else
            {
                _department.Update(model);
            }
            _department.Commit();
            return Json(new ResponseModel()
            {
                Status = httpStatu.Success.ToString(),
                msg = "添加成功"
            });
        }

        /// <summary>
        /// 绑定用户和部门关系
        /// </summary>
        /// <returns></returns>
        public IActionResult Bind()
        {
            return Json("");
        }

        /// <summary>
        /// 部门Data
        /// </summary>
        /// <returns></returns>
        public IActionResult Data(SearchModel model)
        {

            return Json(_user.GetPaper(model));
        }
        //[Route]
        public IActionResult bindUser(int id)
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">部门id</param>
        /// <param name="userids">用户id</param>
        /// <returns></returns>
        public IActionResult SaveBindUser(int id, string userids)
        {
            var sql = $"update Sys_Users set DepartmentId ={id} where Id in ({userids})";
            _user.ExecuteSql(sql, null);
            return Json("");
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="DepartId"></param>
        /// <returns></returns>
        public IActionResult DelDep(int id = 0)
        {
            _department.DeleteWhere(x => x.Id == id);
            return Json("");
        }

        /// <summary>
        /// 获取树data
        /// </summary>
        /// <returns></returns>
        public IActionResult TreeData(string id)
        {
            List<TreeModel> _depList = _department.GetTrees(id);
            return Json(_depList);
        }

        /// <summary>
        /// 获取树data
        /// </summary>
        /// <returns></returns>
        public IActionResult DTreeData(string id)
        {

            return Json(_department.GetDTree());
        }

        public IActionResult treeinput()
        {
            var _Datals = _department.List(x => x.Deleted == false);
            List<TreeModel> trees = _Datals.Where(x => x.ParentId == 0).Select(z => new TreeModel()
            {
                id = z.Id.ToString(),
                name = z.DepartmentName,
                open = true,
                children = _Datals.Where(p => p.ParentId == z.Id).Select(y => new TreeModel()
                {
                    id = z.Id.ToString(),
                    name = z.DepartmentName,
                    open = true,
                }).ToList()
            }).ToList(); ;

            //List<TreeModel> _ls = new List<TreeModel>() {
            //    new TreeModel(){
            //           id="0",
            //           name="节点0",
            //           open=true,
            //           children=new  List<TreeModel>(){
            //               new TreeModel(){
            //                             id="1",
            //                             name="节点1",
            //                             open=true,
            //                             children= new List<TreeModel>(){
            //                                       new TreeModel(){
            //                                             id="2",
            //                                             name="节点2",
            //                                             open=true
            //                             }
            //               }
            //           }
            //           }
            //    }
            //};
            return Json(trees);
        }


        //public IActionResult TreeDataWithInput()
        //{

        //}
        /// <summary>
        /// 部门和用户
        /// </summary>
        /// <returns></returns>
        public IActionResult DataWithUser()
        {
            return Json("");
        }


    }
}