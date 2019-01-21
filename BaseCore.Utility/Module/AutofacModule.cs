using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCore.Utility
{
    public class AutofacModule
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 目前类型，完整路径
        /// </summary>
        public string ToTypeName { get; set; }
        /// <summary>
        /// 接口名称，完整路径
        /// </summary>
        public string FromTypeName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string About { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsUsed { get; set; }
    }
}
