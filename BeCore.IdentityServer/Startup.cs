using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCore.Infrastructure.Data;
using BaseCore.Infrastructure.Repositories;
using BeCore.Core.Interfaces;
using BeCore.IdentityServer.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace BeCore.IdentityServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region 数据库注入到管道中
            var connection = Configuration.GetConnectionString("MySqlConnection");
            services.AddDbContext<BaseContext>(options => options.UseMySql(connection));
            #endregion

            #region IdentityServer配置
            services
                .AddIdentityServer()
                ////如果是production farm使用AddSigningCredential()这个方法.  AddDeveloperSigningCredential 这个方法单纯是为了 让我们的token 保存在硬盘当中。
                .AddDeveloperSigningCredential()//这里是指的使用开发单例
                .AddInMemoryIdentityResources(OuathSetting.GetIdentityResources)//可以对外传出的参数
                .AddTestUsers(OuathSetting.Users.ToList())//可以访问的测试用户  待会需要删掉 调用数据库权限
                .AddInMemoryClients(OuathSetting.Clients)//可以访问的客户端
                .AddInMemoryApiResources(OuathSetting.ApiResources);
            //.AddProfileService<CustomProfileService>()
            //.AddResourceOwnerValidator<CustomResourceOwnerPasswordValidator>()
            #endregion

            #region 配置依赖注入
            services.AddScoped<ISys_UsersRepository, Sys_UsersCodeRepository>();
            #endregion

            #region 添加mvc管道  以及json 配置
            services.AddMvc().AddJsonOptions(options => { options.SerializerSettings.ContractResolver = new DefaultContractResolver(); }); ;
            #endregion

            #region 设置跨域信息
            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("http://localhost:5002")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseIdentityServer();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
