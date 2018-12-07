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
    public class Sys_DepartmentsRepository : EfRepository<Sys_Departments>, ISys_DepartmentsRepository
    {
        private BaseContext baseContext;
        public Sys_DepartmentsRepository(BaseContext dbcontext) : base(dbcontext)
        {
            baseContext = dbcontext;
        }

        public List<TreeModel> GetTrees(string id)
        {
            var _DepLs = this.List(x => x.Deleted == false).ToList();
            List<TreeModel> trees = new List<TreeModel>()
            {
                new TreeModel(){
                    id="0",
                    name="部门管理",
                    open=true,
                    children =  GetSecondTree(0, _DepLs,id)
                }
            };
            return trees;
        }

        public List<TreeModel> GetSecondTree(int parentId, List<Sys_Departments> allData, string selid)
        {
            return allData.Where(x => x.ParentId == parentId).OrderByDescending(z => z.Sortnum).Select(x => new TreeModel()
            {
                id = x.Id.ToString(),
                name = x.DepartmentName,
                alias = x.DepartmentName,
                children = GetSecondTree(x.Id, allData, selid)
            }).ToList();
        }

        public DtreeModel GetDTree()
        {

            var _allLs = List(p => p.Deleted == false).ToList();
            DtreeModel dtreeModel = new DtreeModel()
            {
                status = new statusModel()
                {
                    code = 200,
                    message = "成功"
                },
                data = _allLs.Where(p => p.ParentId == 0).Select(g => new DataLs()
                {
                    id = g.Id.ToString(),
                    title = g.DepartmentName,
                    parentId = "0",
                    isLast = false,
                    checkArr = new List<checkArr>
                            {
                                new checkArr(){ type="0",isChecked="0"}
                            },
                    children = getSecond(_allLs, g.Id)
                })
            };
            return dtreeModel;
        }

        public List<DataLs> getSecond(List<Sys_Departments> _all, int parentid)
        {
            return _all.Where(p => p.ParentId == parentid).Select(g => new DataLs()
            {
                id = g.Id.ToString(),
                title = g.DepartmentName,
                parentId = parentid.ToString(),
                isLast = false,
                checkArr = new List<checkArr>
                            {
                                new checkArr(){ type="0",isChecked="0"}
                            },
                children = getSecond(_all, g.Id)
            }).ToList();
        }
    }
}
