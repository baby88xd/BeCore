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
    //public class Sys_DepartmentsRepository : EfRepository<Sys_Departments>, ISys_DepartmentsRepository
    public class Sys_NavigationsRepository : EfRepository<Sys_Navigations>, ISys_NavigationsRepository
    {
        private BaseContext baseContext;
        public Sys_NavigationsRepository(BaseContext dbcontext) : base(dbcontext)
        {
            baseContext = dbcontext;
            //ISys_ButtonsRepository Sys_ButtonsRepository = new Sys_ButtonsRepository(baseContext);

        }



        public List<TreeModel> GetTrees(string id)
        {
            var _AllData = List(x => x.Deleted == false).ToList();
            List<TreeModel> trees = new List<TreeModel>()
            {
                                new TreeModel(){
                    id="0",
                    name="菜单管理",
                    open=true,
                    children =  GetSecondTree(0, _AllData,id)
                }
            };

            return trees;
            //throw new NotImplementedException();
        }

        public List<TreeModel> GetSecondTree(int parentId, List<Sys_Navigations> allData, string selid)
        {
            return allData.Where(x => x.ParentID == parentId).OrderByDescending(z => z.Sortnum).Select(x => new TreeModel()
            {
                id = x.Id.ToString(),
                name = x.NavTitle,
                alias = x.NavTitle,
                children = GetSecondTree(x.Id, allData, selid)
            }).ToList();
        }

        public string GetNavHtml()
        {
            var _allData = List(x => x.Deleted == false).ToList();
            StringBuilder _sbhtml = new StringBuilder();
            foreach (var _First in _allData.Where(x => x.ParentID == 0))
            {
                _sbhtml.AppendLine($"<li data-name=\"{_First.NavTag}\" class=\"layui-nav-item\">");
                if (_allData.Where(z => z.ParentID == _First.Id).Count() > 0) //有子节点
                {
                    _sbhtml.AppendLine($"<a href=\"javascript:; \" lay-tips=\"{_First.NavTitle}\" lay-direction=\"2\">");
                    if (string.IsNullOrEmpty(_First.iconUrl))//没有自定义样式
                        _sbhtml.AppendLine($"<i class=\"layui-icon layui-icon-component\"></i>");
                    else//有自定义样式
                        _sbhtml.AppendLine($"<i class=\"layui-icon {_First.iconCls}\"></i>");
                    _sbhtml.AppendLine($"<cite>{_First.NavTitle}</cite>");
                    _sbhtml.AppendLine("</a>");
                    _sbhtml.AppendLine(GetSecond(_allData, _First.Id));
                }
                else //没有子节点
                {
                    _sbhtml.AppendLine($"<li data-name=\"get\" class=\"layui-nav-item\">");
                    _sbhtml.AppendLine($"<a href=\"javascript:;\" lay-href=\"{_First.Linkurl}\" lay-tips=\"{_First.NavTitle}\" lay-direction=\"2\">");
                    if (string.IsNullOrEmpty(_First.iconUrl))//没有自定义样式
                        _sbhtml.AppendLine($"<i class=\"layui-icon layui-icon-auz\"></i>");
                    else
                        _sbhtml.AppendLine($"<i class=\"layui-icon {_First.iconCls}\"></i>");
                    _sbhtml.AppendLine($"<cite>{_First.NavTitle}</cite>");
                    _sbhtml.AppendLine($"</a>");


                }
                _sbhtml.AppendLine("<li>");
            }
            return _sbhtml.ToString();
        }


        public string GetSecond(List<Sys_Navigations> all, int parentid)
        {
            StringBuilder PartialHtml = new StringBuilder();
            PartialHtml.AppendLine($"<dl class=\"layui-nav-child\">");
            foreach (var _sel in all.Where(p => p.ParentID == parentid))
            {
                if (all.Where(z => z.ParentID == _sel.Id).Count() > 0)
                {
                    PartialHtml.AppendLine($"<dd data-name=\"form\">");
                    PartialHtml.AppendLine($" <a href=\"javascript:;\">表单</a>");
                    PartialHtml.AppendLine(GetSecond(all, _sel.Id));
                    PartialHtml.AppendLine($" </dd>");

                }
                else
                {
                    PartialHtml.AppendLine($"<dd data-name=\"{_sel.NavTag}\"><a lay-href=\"{_sel.Linkurl}\">{_sel.NavTitle}</a></dd>");
                }
            }

            PartialHtml.AppendLine();
            PartialHtml.AppendLine();
            PartialHtml.AppendLine("</dl>");

            return PartialHtml.ToString();
        }

        public DtreeModel GetDtree()
        {
            var _allLs = List(p => p.Deleted == false).ToList();
            var _aim = new DtreeModel()
            {
                status = new statusModel()
                {
                    code = 200,
                    message = "成功"
                },
                data = _allLs.Where(z => z.ParentID == 0).Select(g => new DataLs()
                {
                    id = g.Id.ToString(),
                    title = g.NavTitle,
                    parentId = g.ParentID.ToString(),
                    isLast = false,
                    children = GetSecondDtree(_allLs, g.Id)
                })
            };
            return _aim;
        }

        public List<DataLs> GetSecondDtree(List<Sys_Navigations> all, int parentid)
        {
            List<DataLs> _aim = new List<DataLs>();
            _aim = all.Where(m => m.ParentID == parentid).Select(g => new DataLs()
            {
                id = g.Id.ToString(),
                title = g.NavTitle,
                parentId = g.ParentID.ToString(),
                isLast = false,
                children = GetSecondDtree(all, g.Id)
            }).ToList();
            return _aim;
        }

        public DtreeModel GetDtreeBtn(int RoleId)
        {
            //先获取roleid 下有的 按钮和菜单 t
            //获取所有的按钮
            //TODO:开始做 。 我凑 2018-12-04 19：50 开始的 我想看看结束时间是啥时候
            ISys_ButtonsRepository Sys_ButtonsRepository = new Sys_ButtonsRepository(baseContext);//所有按钮
            ISys_NavButtonsRepository sys_NavButtons = new Sys_NavButtonsRepository(baseContext);//所有关联关系
            ISys_RoleNavBtnsRepository _roleNavBtn = new Sys_RoleNavBtnsRepository(baseContext);
            //获取roleId 有的Nav 和 btn
            var roleNavBtnls = _roleNavBtn.List(p => p.RoleId == RoleId).ToList();
            //获取Button列表
            var _allBtn = Sys_ButtonsRepository.List(z => z.Deleted == false).ToList();
            //获取菜单和按钮的关系
            var NavBtn = sys_NavButtons.List(z => z.Deleted == false).ToList();
            //获取所有的菜单
            var Navs = List(z => z.Deleted == false).ToList();

            var _aim = new DtreeModel()
            {
                status = new statusModel()
                {
                    code = 200,
                    message = "成功"
                },
                data = Navs.Where(z => z.ParentID == 0).Select(g => new DataLs()
                {
                    id = "Nav_" + g.Id.ToString(),
                    title = g.NavTitle,
                    parentId = g.ParentID.ToString(),
                    isLast = (Navs.Where(z => z.ParentID == g.Id).Count() > 0 && NavBtn.Where(z => z.NavId == g.Id).Count() > 0),
                    checkArr = new List<checkArr>()
                            {
                                new checkArr(){ type="0",isChecked="0"}
                            },
                    children = GetSecondDtreeBtn(roleNavBtnls, _allBtn, NavBtn, Navs, g.Id)
                })
            };
            return _aim;
        }
        public List<DataLs> GetSecondDtreeBtn(List<Sys_RoleNavBtns> roleNavBtnls, List<Sys_Buttons> _allBtn, List<Sys_NavButtons> NavBtn, List<Sys_Navigations> Navs, int parentid)
        {
            List<DataLs> _result = new List<DataLs>();
            //获取当前菜单的按钮
            var Btnids = NavBtn.Where(p => p.NavId == parentid).Select(z => z.ButtonId).ToList();
            var _selBtn = _allBtn.Where(p => Btnids.Contains(p.Id)).Select(z => new DataLs()
            {
                id = $"Btn_{parentid}_{z.Id}",
                title = z.ButtonText,
                parentId = parentid.ToString(),
                isLast = true,
                checkArr = new checkArr() { type = "0", isChecked = "0" }
            });
            _result.AddRange(_selBtn);
            var Navls = Navs.Where(z => z.ParentID == parentid).Select(g => new DataLs()
            {
                id = "Nav_" + g.Id.ToString(),
                title = g.NavTitle,
                parentId = g.ParentID.ToString(),
                isLast = (Navs.Where(z => z.ParentID == g.Id).Count() > 0 && NavBtn.Where(z => z.NavId == g.Id).Count() > 0),
                checkArr = new List<checkArr>
                            {
                                new checkArr(){ type="0",isChecked="0"}
                            },
                children = GetSecondDtreeBtn(roleNavBtnls, _allBtn, NavBtn, Navs, g.Id)
            });
            _result.AddRange(Navls);
            return _result;


        }
    }
}
