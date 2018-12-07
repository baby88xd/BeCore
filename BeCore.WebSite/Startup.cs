using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCore.Infrastructure.Data;
using BaseCore.Infrastructure.Repositories;
using BeCore.Core.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace BeCore.WebSite
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
            //数据库链接
            var connection = Configuration.GetConnectionString("MySqlConnection");
            services.AddDbContext<BaseContext>(options => options.UseMySql(connection));

            services.AddScoped<IBussCodeInfoRepository, BussCodeInfoRepository>();
            services.AddScoped<IBussCodeRepository, BussCodeRepository>();
            services.AddScoped<ISys_UsersRepository, Sys_UsersCodeRepository>();
            services.AddScoped<ISys_DepartmentsRepository, Sys_DepartmentsRepository>();
            services.AddScoped<ISys_ButtonsRepository, Sys_ButtonsRepository>();
            services.AddScoped<ISys_NavigationsRepository, Sys_NavigationsRepository>();
            services.AddScoped<ISys_RoleNavBtnsRepository, Sys_RoleNavBtnsRepository>();
            services.AddScoped<ISys_NavButtonsRepository, Sys_NavButtonsRepository>();
            services.AddScoped<ISys_RolesRepository, Sys_RolesRepository>();

            services.AddMvc().AddJsonOptions(options => { options.SerializerSettings.ContractResolver = new DefaultContractResolver(); });
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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
