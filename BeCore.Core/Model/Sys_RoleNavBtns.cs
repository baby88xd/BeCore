using BeCore.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCore.Core.Model
{
    public class Sys_RoleNavBtns : EntityBase
    {
        public int RoleId { get; set; }
        public int NavId { get; set; }
        public int BtnId { get; set; }

    }
}
