using BeCore.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCore.Core.Model
{
    public class Sys_NavButtons : EntityBase
    {
        public int NavId { get; set; }
        public int ButtonId { get; set; }
        public string Sortnum { get; set; }

    }
}
