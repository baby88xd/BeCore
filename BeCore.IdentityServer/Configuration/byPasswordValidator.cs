using BeCore.Core.Interfaces;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BeCore.IdentityServer.Configuration
{
    public class ByPasswordValidator : IResourceOwnerPasswordValidator
    {
        /// <summary>
        /// 这里为了演示我们还是使用TestUser作为数据源，
        /// 正常使用此处应当传入一个 用户仓储 等可以从
        /// 数据库或其他介质获取我们用户数据的对象
        /// </summary>
        private ISys_UsersRepository _users { get; set; }
        private readonly ISystemClock _clock;

        public ByPasswordValidator(ISys_UsersRepository _sys_Users, ISystemClock clock)
        {
            _users = _sys_Users;
            _clock = clock;
        }
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            //查找当前用户是否存在
            var user = _users.List(z => !z.Deleted && z.UserName == context.UserName).FirstOrDefault();
            if (user != null)//用户存在
            {
                if (user.UserName == context.UserName && context.Password.Md5Random(user.PassSalt) == user.Password)
                {
                    //用户可用
                    //验证通过返回结果 
                    //subjectId 为用户唯一标识 一般为用户id
                    //authenticationMethod 描述自定义授权类型的认证方法 
                    //authTime 授权时间
                    //claims 需要返回的用户身份信息单元 此处应该根据我们从数据库读取到的用户信息 添加Claims 如果是从数据库中读取角色信息，那么我们应该在此处添加 此处只返回必要的Claim
                    context.Result = new GrantValidationResult(
                                user.Id.ToString() ?? throw new ArgumentException("Subject ID not set", ""),
                                  OidcConstants.AuthenticationMethods.Password,
                                   _clock.UtcNow.UtcDateTime,
                                    new[] {
                                        new Claim("email", user.Email)
                                    }
                        );
                    //context.Result = new GrantValidationResult(
                    //    user.SubjectId ?? throw new ArgumentException("Subject ID not set", nameof(user.SubjectId)),
                    //    OidcConstants.AuthenticationMethods.Password, _clock.UtcNow.UtcDateTime,
                    //    user.Claims);
                }
                else
                {
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "invalid custom credential");
                }
            }
            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "invalid custom credential");
            }
            return Task.CompletedTask;

        }
    }
}
