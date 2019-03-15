using BeCore.Core.Interfaces;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BeCore.IdentityServer.Configuration
{


    /// <summary>
    /// 参考内容
    /// https://www.cnblogs.com/stulzq/p/8726002.html
    /// </summary>
    public class ProfileService : IProfileService
    {
        private ISys_UsersRepository _users { get; set; }
        public ProfileService(ISys_UsersRepository _sys_Users)
        {
            _users = _sys_Users;
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            if (context.RequestedClaimTypes.Any())
            {
                //string id = context.Subject.GetSubjectId();
                //var user = Users.FindBySubjectId(context.Subject.GetSubjectId());
                //if (user != null)
                //{
                //调用此方法以后内部会进行过滤，只将用户请求的Claim加入到 context.IssuedClaims 集合中 这样我们的请求方便能正常获取到所需Claim
                var id = context.Subject.GetSubjectId();
                context.AddRequestedClaims(new[] {
                    new Claim("email", "451303407@qq.com"),
                    new Claim("test", "123") }
                );
                //}
            }
            //if (context.IssuedClaims.Count == 0)
            //{
            //    if (context.Subject.Claims.Count() > 0)
            //    {
            //        context.IssuedClaims = context.Subject.Claims.ToList();
            //    }
            //}
            return Task.CompletedTask;
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            return Task.CompletedTask;
        }
    }
}
