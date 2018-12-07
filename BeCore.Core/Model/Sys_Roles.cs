using BeCore.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCore.Core.Model
{
    public class Sys_Roles : EntityBase
    {
        public string RoleName { get; set; }
        public string Sortnum { get; set; }
        public string Remark { get; set; }
        public bool IsDefault { get; set; }

    }
}
