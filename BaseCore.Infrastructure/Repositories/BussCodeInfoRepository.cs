using BaseCore.Infrastructure.Data;
using BaseCore.Utility.HelperModel;
using BeCore.Core.Interfaces;
using BeCore.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BaseCore.Infrastructure.Repositories
{

    public class BussCodeInfoRepository : EfRepository<BussCodeInfo>, IBussCodeInfoRepository
    {
        private BaseContext baseContext;
        public BussCodeInfoRepository(BaseContext dbcontext) : base(dbcontext)
        {
            baseContext = dbcontext;
        }
    }
}
