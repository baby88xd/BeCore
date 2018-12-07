using BeCore.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCore.Core.Model
{
    public class Sys_Users : EntityBase
    {

        /// <summary>
        /// 用户登陆名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 密码Salt
        /// </summary>
        public string PassSalt { get; set; }
        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool IsAdmin { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsDisabled { get; set; }
        /// <summary>
        /// 用户真实名称
        /// </summary>
        public string TrueName { get; set; }
        /// <summary>
        /// 所属部门
        /// </summary>
        public int DepartmentId { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// QQ号
        /// </summary>
        public string QQ { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 配置json
        /// </summary>
        public string ConfigJson { get; set; }

    }
}
