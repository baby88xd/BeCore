using BeCore.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCore.Core.Model
{
    public class Sys_UserRoles : EntityBase
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }

    }
}
