using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BeCore.IdentityServer.Configuration
{
    public class OuathSetting
    {
        /// <summary>
        /// 可以访问的ApiResources
        /// </summary>
        /// <Remark>这里是一个类似于 域 的东西</Remark>
        public static IEnumerable<ApiResource> ApiResources
        {
            get
            {
                return new[]
                {

                new ApiResource("socialnetwork", "社交网络")
                {
                                         UserClaims = new [] { "email" }
                }
            };
            }
        }

        /// <summary>
        /// 获取可以访问的客户端
        /// </summary>
        public static IEnumerable<Client> Clients
        {
            get
            {
                return new[]
                {
                new Client
                {
                    ClientId = "socialnetwork",
                    ClientSecrets = new [] { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = new [] { "socialnetwork" }
                },
                new Client
                {
                    ClientId = "mvc_implicit",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = { "http://localhost:5002/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                         "socialnetwork"
                    },
                    AllowAccessTokensViaBrowser = true //修改Authorization Server的Client来允许返回Access Token
                   
                },
                new Client
                {
                    ClientId = "mvc_code",
                    ClientName = "MVC Code Client",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    RedirectUris = { "http://localhost:5002/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "socialnetwork"
                    },
                    AllowOfflineAccess = true,//允许离线工作
                    AllowAccessTokensViaBrowser = true
                },
                new Client
                {
                    ClientId = "js",
                    ClientName = "JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = { "http://localhost:5002/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },
                    //RedirectUris =           { "http://localhost:5003/callback.html" },
                    //PostLogoutRedirectUris = { "http://localhost:5003/index.html" },
                    AllowedCorsOrigins =     { "http://localhost:5003" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
            };
            }
        }

        /// <summary>
        /// 返回可以获取到的信息
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResources
        {
            get
            {
                return new List<IdentityResource>
                {
                    new IdentityResources.OpenId(),
                    new IdentityResources.Profile(),
                    new IdentityResources.Email()
                };
            }
        }

        /// <summary>
        /// 可以登录的用户
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<TestUser> Users
        {
            get
            {
                return new[]
                {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "mail@qq.com",
                    Password = "password",
                    Claims = new [] { new Claim("email", "mail@qq.com") }
                }
            };
            }
        }
    }
}
