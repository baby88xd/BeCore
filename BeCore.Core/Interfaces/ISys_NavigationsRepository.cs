using BaseCore.Utility.HelperModel;
using BeCore.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeCore.Core.Interfaces
{
    public interface ISys_NavigationsRepository : IRepository<Sys_Navigations>
    {
        List<TreeModel> GetTrees(string id);

        string GetNavHtml();
        DtreeModel GetDtree();


        DtreeModel GetDtreeBtn(int RoleId);

    }
}
