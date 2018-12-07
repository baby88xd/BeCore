using BeCore.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCore.Core.Model
{
    public class BussCodeInfo : EntityBase
    {
        /// <summary>
        /// 父节点ID
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// 语言的Code
        /// </summary>

        public string LanguageCode { get; set; }
        /// <summary>
        /// 语言的描述
        /// </summary>
        public string LanguageRemark { get; set; }
        /// <summary>
        /// 提示信息
        /// </summary>
        public string MsgInfo { get; set; }
    }
}
