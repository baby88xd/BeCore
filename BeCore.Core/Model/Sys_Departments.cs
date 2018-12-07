using BeCore.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCore.Core.Model
{
    public class Sys_Departments : EntityBase
    {
        public string DepartmentName { get; set; }
        public int ParentId { get; set; }
        public string Sortnum { get; set; }
        public string Remark { get; set; }

    }
}
