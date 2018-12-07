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
    public class UserController : Controller
    {
        private ISys_UsersRepository sys_Users { get; set; }
        public UserController(ISys_UsersRepository _sys_Users)
        {
            sys_Users = _sys_Users;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Data(SearchModel model)
        {
            return Json(sys_Users.GetPaper(model));
        }
        [HttpGet]
        public IActionResult Add(int id)
        {
            if (id == 0)
            {
                return View(new BeCore.Core.Model.Sys_Users()
                {
                    AddTime = DateTime.Now,
                    Deleted = false
                });
            }
            if (id != 0)
            {
                return View(sys_Users.List(x => x.Id == id).FirstOrDefault());
            }
            return View();
        }
        [HttpPost]
        public IActionResult Add(Sys_Users model)
        {
            if (model.Id == 0)
            {
                model.PassSalt = Guid.NewGuid().ToString().Substring(0, 4);
                model.Password = model.Password.Md5Random(model.PassSalt);
                sys_Users.Add(model);
            }
            else
            {
                model.Password = model.Password.Md5Random(model.PassSalt);
                sys_Users.Update(model);
            }
            sys_Users.Commit();
            return Json(new ResponseModel()
            {
                Status = "Success"
            });
        }
        [HttpGet]
        public IActionResult Del(int id)
        {
            sys_Users.DeleteWhere(x => x.Id == id, true);
            return Json(new ResponseModel()
            {
                Status = "Success"
            });
        }

        public IActionResult EditePass(int id, string password)
        {
            var _userModel = sys_Users.List(x => x.Id == id).FirstOrDefault();
            _userModel.Password = password.Md5Random(_userModel.PassSalt);
            sys_Users.Update(_userModel);

            return Json(new ResponseModel()
            {
                Status = "Success"
            });
        }
    }
}