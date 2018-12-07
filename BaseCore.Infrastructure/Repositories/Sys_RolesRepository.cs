using BaseCore.Infrastructure.Data;
using BeCore.Core.Interfaces;
using BeCore.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCore.Infrastructure.Repositories
{
    public class Sys_RolesRepository : EfRepository<Sys_Roles>, ISys_RolesRepository
    {
        private BaseContext baseContext;
        public Sys_RolesRepository(BaseContext dbcontext) : base(dbcontext)
        {
            baseContext = dbcontext;
        }
    }
}
