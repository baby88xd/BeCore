using BeCore.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCore.Core.Model
{
    public class Sys_UserNavBtns : EntityBase
    {
        public int UserId { get; set; }
        public int NavId { get; set; }
        public int BtnId { get; set; }

    }
}
