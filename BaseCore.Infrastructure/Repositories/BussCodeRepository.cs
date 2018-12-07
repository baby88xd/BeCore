using BaseCore.Infrastructure.Data;
using BaseCore.Utility.HelperModel;
using BeCore.Core.Interfaces;
using BeCore.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseCore.Infrastructure.Repositories
{
    public class BussCodeRepository : EfRepository<BussCode>, IBussCodeRepository
    {
        private BaseContext baseContext;
        public BussCodeRepository(BaseContext dbcontext) : base(dbcontext)
        {
            baseContext = dbcontext;
        }

        public List<TreeModel> GetTrees()
        {
            var AllData = List(x => x.Deleted == false).ToList();
            var _returnData = AllData.Where(e => e.IsPublic == false)
                .GroupBy(x => new { x.ApplicationId, x.ApplicationName }).Select(z => new TreeModel()
                {
                    id = "ApplicationId" + z.Key.ApplicationId,
                    name = $"[{z.Key.ApplicationId}]{z.Key.ApplicationName}",
                    alias = $"[{z.Key.ApplicationId}]{z.Key.ApplicationName}",
                    children = AllData.Where(a => !a.Deleted && a.IsPublic == false && a.ApplicationId == z.Key.ApplicationId).Select(p => new TreeModel()
                    {
                        id = p.Id.ToString(),
                        name = $"[{p.CodeNum}]{p.CodeRemark}",
                        alias = $"[{p.CodeNum}]{p.CodeRemark}"
                    })
                }).ToList();
            _returnData.Add(new TreeModel()
            {
                id = "ApplicationIdPublic",
                name = "公用",
                alias = "公用",
                children = AllData.Where(a => !a.Deleted && a.IsPublic == true).Select(p => new TreeModel()
                {
                    id = p.Id.ToString(),
                    name = $"[{p.CodeNum}]{p.CodeRemark}",
                    alias = $"[{p.CodeNum}]{p.CodeRemark}"
                })
            });

            return _returnData;
        }
    }
}
