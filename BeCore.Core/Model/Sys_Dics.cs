using BeCore.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCore.Core.Model
{
    public class Sys_Dics : EntityBase
    {
        public string Title { get; set; }
        public string Code { get; set; }
        public string Sortnum { get; set; }
        public string ParentId { get; set; }
        public string CategoryId { get; set; }
        public string Remark { get; set; }
        public string Status { get; set; }

    }
}
