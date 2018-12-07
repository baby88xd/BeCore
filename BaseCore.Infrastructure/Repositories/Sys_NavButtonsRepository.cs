using BaseCore.Infrastructure.Data;
using BeCore.Core.Interfaces;
using BeCore.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCore.Infrastructure.Repositories
{
    public class Sys_NavButtonsRepository : EfRepository<Sys_NavButtons>, ISys_NavButtonsRepository
    {
        private BaseContext baseContext;
        public Sys_NavButtonsRepository(BaseContext dbcontext) : base(dbcontext)
        {
            baseContext = dbcontext;
        }
    }
}
