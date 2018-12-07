using BaseCore.Infrastructure.Data;
using BeCore.Core.Interfaces;
using BeCore.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCore.Infrastructure.Repositories
{

    public class Sys_UsersCodeRepository : EfRepository<Sys_Users>, ISys_UsersRepository
    {
        private BaseContext baseContext;
        public Sys_UsersCodeRepository(BaseContext dbcontext) : base(dbcontext)
        {
            baseContext = dbcontext;
        }
    }
}
