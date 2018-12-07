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
    public class Sys_ButtonsRepository : EfRepository<Sys_Buttons>, ISys_ButtonsRepository
    {
        private BaseContext baseContext;
        public Sys_ButtonsRepository(BaseContext dbcontext) : base(dbcontext)
        {
            baseContext = dbcontext;
        }

        public DtreeModel GetDtree()
        {
            var _allLs = List(p => p.Deleted == false).ToList();
            DtreeModel _aim = new DtreeModel()
            {
                status = new statusModel()
                {
                    code = 200,
                    message = "成功"
                },
                data = _allLs.Select(g => new DataLs()
                {
                    id = g.Id.ToString(),
                    title = g.ButtonText,
                    parentId = "0",
                    isLast = false,
                    checkArr = new List<checkArr>
                            {
                                new checkArr(){ type="0",isChecked="0"}
                            }
                })
            };
            return _aim;
        }
    }
}
