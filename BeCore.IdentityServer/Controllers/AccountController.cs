using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using BeCore.Core;
using BeCore.Core.Interfaces;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
// 抄袭的地址:https://www.cnblogs.com/wyt007/p/8309377.html
namespace BeCore.IdentityServer.Controllers
{

    public class AccountController : Controller
    {
        private ISys_UsersRepository sys_Users { get; set; }
        //private readonly TestUserStore _users;
        public AccountController(ISys_UsersRepository _sys_Users)
        {
            //_users = users;
            sys_Users = _sys_Users;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 内部跳转
        /// </summary>
        /// <param name="returnUrl">跳转到的url地址</param>
        /// <returns></returns>
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {//如果是本地
                return Redirect(returnUrl);
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }


        /// <summary>
        /// 错误验证
        /// </summary>
        /// <param name="result">IdentityResult model</param>
        private void AddError(IdentityResult result)
        {
            //遍历所有的验证错误
            foreach (var error in result.Errors)
            {
                //返回error到model
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        /// <summary>
        /// 注册的地址
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel, string returnUrl = null)
        {
            return View();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                ViewData["returnUrl"] = returnUrl;
                //var user = _users.FindByUsername(loginViewModel.UserName);
                string userName = loginViewModel.UserName;
                //查找当前用户是否存在
                var user = sys_Users.List(z => !z.Deleted && z.UserName == userName).FirstOrDefault();//

                if (user == null)
                {
                    ModelState.AddModelError(nameof(loginViewModel.UserName), "用户名不存在。请检查");
                }
                else
                {
                    //这段代码是test User的 暂时先不用了
                    //if (_users.ValidateCredentials(loginViewModel.UserName, loginViewModel.Password)) 

                    if (user.UserName == loginViewModel.UserName && loginViewModel.Password.Md5Random(user.PassSalt) == user.Password)
                    {
                        //是否记住
                        var prop = new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(30))
                        };

                        await Microsoft.AspNetCore.Http.AuthenticationManagerExtensions.SignInAsync(HttpContext, user.Id.ToString(), user.UserName, prop);
                        //await Microsoft.AspNetCore.Http.AuthenticationManagerExtensions.SignInAsync(HttpContext, testUser.SubjectId, testUser.Username, prop);
                    }
                }

                return RedirectToLocal(returnUrl);
            }

            return View();
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
