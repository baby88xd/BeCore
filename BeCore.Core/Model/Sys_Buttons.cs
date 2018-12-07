using BeCore.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCore.Core.Model
{
    public class Sys_Buttons : EntityBase
    {
        /// <summary>
        /// 按钮名称
        /// </summary>
        public string ButtonText { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public string Sortnum { get; set; }
        /// <summary>
        /// icon 样式
        /// </summary>
        public string iconCls { get; set; }
        /// <summary>
        /// icon链接
        /// </summary>
        public string IconUrl { get; set; }
        /// <summary>
        /// tag code
        /// </summary>
        public string ButtonTag { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// html
        /// </summary>
        public string ButtonHtml { get; set; }

    }
}
