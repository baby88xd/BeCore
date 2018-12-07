using BeCore.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCore.Core.Model
{
    public class Sys_Navigations : EntityBase
    {
        public string NavTitle { get; set; }
        public string Linkurl { get; set; }
        public string Sortnum { get; set; }
        public string iconCls { get; set; }
        public string iconUrl { get; set; }
        public string IsVisible { get; set; }
        public int ParentID { get; set; }
        public string NavTag { get; set; }
        public string BigImageUrl { get; set; }

    }
}
