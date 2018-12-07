using BaseCore.Infrastructure.EntityConfigurations;
using BeCore.Core.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCore.Infrastructure.Data
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions<BaseContext> options)
: base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new BussCodeConfiguration());
            modelBuilder.ApplyConfiguration(new BussCodeInfoConfiguration());
            //系统用户配置文件
            modelBuilder.ApplyConfiguration(new Sys_UsersConfiguration());
            //用户部门管理
            modelBuilder.ApplyConfiguration(new Sys_DepartmentsConfiguration());
            //按钮
            modelBuilder.ApplyConfiguration(new Sys_ButtonsConfiguration());
            //菜单
            modelBuilder.ApplyConfiguration(new Sys_NavigationsConfiguration());
            //菜单和按钮 Role
            modelBuilder.ApplyConfiguration(new Sys_RoleNavBtnsConfiguration());
            //菜单和按钮
            modelBuilder.ApplyConfiguration(new Sys_NavButtonsConfiguration());
            //角色表
            modelBuilder.ApplyConfiguration(new Sys_RolesConfiguration());




        }


        public DbSet<BussCode> BussCode { get; set; }
        public DbSet<BussCodeInfo> BussCodeInfo { get; set; }
        /// <summary>
        /// 系统用户表
        /// </summary>
        public DbSet<Sys_Users> Sys_Users { get; set; }
        /// <summary>
        /// 用户部门表
        /// </summary>
        public DbSet<Sys_Departments> Sys_Departments { get; set; }
        /// <summary>
        /// 系统按钮
        /// </summary>
        public DbSet<Sys_Buttons> Sys_Buttons { get; set; }

        /// <summary>
        /// 菜单和按钮 Role
        /// </summary>
        public DbSet<Sys_RoleNavBtns> sys_RoleNavBtns { get; set; }
        /// <summary>
        /// 菜单和按钮
        /// </summary>
        public DbSet<Sys_NavButtons> sys_NavButtons { get; set; }
        /// <summary>
        /// 角色表
        /// </summary>
        public DbSet<Sys_Roles> sys_Roles { get; set; }



    }
}
