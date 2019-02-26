using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using BaseCore.Infrastructure.Data;
using BaseCore.Infrastructure.Repositories;
using BaseCore.Utility;
using BeCore.Core.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //数据库链接
            var connection = Configuration.GetConnectionString("MySqlConnection");
            services.AddDbContext<BaseContext>(options => options.UseMySql(connection));
            var baseType = typeof(IRepository<Core.Base.EntityBase>);
            var assembly = Assembly.GetEntryAssembly();
            AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName("BaseCore.Infrastructure"));
            AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName("BeCore.Core"));


            //services.AddScoped<IBussCodeInfoRepository, BussCodeInfoRepository>();
            //services.AddScoped<IBussCodeRepository, BussCodeRepository>();
            //services.AddScoped<ISys_UsersRepository, Sys_UsersCodeRepository>();
            //services.AddScoped<ISys_DepartmentsRepository, Sys_DepartmentsRepository>();
            //services.AddScoped<ISys_ButtonsRepository, Sys_ButtonsRepository>();
            //services.AddScoped<ISys_NavigationsRepository, Sys_NavigationsRepository>();
            //services.AddScoped<ISys_RoleNavBtnsRepository, Sys_RoleNavBtnsRepository>();
            //services.AddScoped<ISys_NavButtonsRepository, Sys_NavButtonsRepository>();
            //services.AddScoped<ISys_RolesRepository, Sys_RolesRepository>();
            //IContainer container = builder.Build();
            //var jsonServices = JObject.Parse(File.ReadAllText("appSettings.json"))["DiConfig"];
            //var ModuleList = JsonConvert.DeserializeObject<List<AutofacModule>>(jsonServices.ToString());

            services.AddMvc().AddJsonOptions(options => { options.SerializerSettings.ContractResolver = new DefaultContractResolver(); });
            var builder = new ContainerBuilder();//实例化 AutoFac  容器    

            IConfigurationBuilder config = new ConfigurationBuilder();
            config.AddJsonFile("autofac.json");
            //.AddJsonFile($"Test.json", optional: true, reloadOnChange: true)
            var module = new ConfigurationModule(config.Build());
            //var containerBuilder = new ContainerBuilder();
            builder.RegisterModule(module);
            builder.Populate(services); ;//管道寄居

            //builder.Populate(services)

            #region autofac 配置文件注入 2019-1-15 废除
            //builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
            //BeCore.Core.Interfaces.ISys_NavigationsRepository
            //ApplicationContainer = builder.Build();
            //var ModuleList = new List<AutofacModule>();// new AutofacModule();
            //BeCore.Core.Interfaces.ISys_NavigationsRepository
            //BeCore.Core.Interfaces.ISys_NavigationsRepository
            //
            //ModuleList.Add(new AutofacModule { ID = 1, FromTypeName = "BeCore.Core.Interfaces.ISys_NavigationsRepository", ToTypeName = "BaseCore.Infrastructure.Repositories.Sys_NavigationsRepository" });
            //ModuleList.Add(new AutofacModule { ID = 1, FromTypeName = "Progame.IMul", ToTypeName = "Program.Mul2" });
            //var builder = new ContainerBuilder();
            //foreach (var item in ModuleList)
            //{
            //    var AppDomains = AppDomain.CurrentDomain.GetAssemblies()
            //          .SelectMany(a => a.GetTypes());
            //    var zzz = AppDomains.Select(p => p.FullName.ToString()).Where(p => p.Contains("Sys_NavigationsRepository")).ToList();
            //    var fromType = AppDomains
            //          .Where(i => i.FullName == item.FromTypeName).FirstOrDefault();

            //    var toType = AppDomains
            //       .Where(i => i.FullName == item.ToTypeName).FirstOrDefault();

            //    builder.RegisterType(toType).Named(toType.FullName, fromType).InstancePerDependency();
            //}
            #endregion
            ApplicationContainer = builder.Build();


            return new AutofacServiceProvider(ApplicationContainer);//将autofac反馈到管道中
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
