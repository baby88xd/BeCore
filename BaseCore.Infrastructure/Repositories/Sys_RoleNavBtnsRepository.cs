using BaseCore.Infrastructure.Data;
using BeCore.Core.Interfaces;
using BeCore.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCore.Infrastructure.Repositories
{
    //  public class Sys_ButtonsRepository : EfRepository<Sys_Buttons>, ISys_ButtonsRepository
    public class Sys_RoleNavBtnsRepository : EfRepository<Sys_RoleNavBtns>, ISys_RoleNavBtnsRepository
    {
        private BaseContext baseContext;
        public Sys_RoleNavBtnsRepository(BaseContext dbcontext) : base(dbcontext)
        {
            baseContext = dbcontext;
        }
    }
}
