using BaseCore.Utility.HelperModel;
using BeCore.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCore.Core.Interfaces
{
    public interface ISys_DepartmentsRepository : IRepository<Sys_Departments>
    {
        List<TreeModel> GetTrees(string id);

        DtreeModel GetDTree();
    }
}
