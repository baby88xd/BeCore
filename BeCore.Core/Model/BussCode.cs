using BeCore.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCore.Core.Model
{
    public class BussCode : EntityBase
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string CodeNum { get; set; }
        /// <summary>
        /// 编码描述
        /// </summary>
        public string CodeRemark { get; set; }
        /// <summary>
        /// 系统ID
        /// </summary>
        public string ApplicationId { get; set; }
        /// <summary>
        /// 系统描述
        /// </summary>
        public string ApplicationName { get; set; }
        /// <summary>
        /// 是否公有
        /// </summary>
        public bool IsPublic { get; set; }

    }
}
