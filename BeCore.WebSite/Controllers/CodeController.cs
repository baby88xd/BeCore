using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BaseCore.Utility.HelperModel;
using BeCore.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BeCore.WebSite.Controllers
{
    public class CodeController : Controller
    {
        //services.AddScoped<IBussCodeInfoRepository, BussCodeInfoRepository>();
        //    services.AddScoped<IBussCodeRepository, BussCodeRepository>();
        private IBussCodeInfoRepository _bussCodeInfo { get; set; }
        private IBussCodeRepository _bussCode { get; set; }
        public CodeController(IBussCodeInfoRepository bussCodeInfo, IBussCodeRepository bussCode)
        {
            _bussCode = bussCode;
            _bussCodeInfo = bussCodeInfo;
        }
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public IActionResult TreeData()
        {
            return Json(_bussCode.GetTrees());
        }
        /// <summary>
        /// 列表数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult GridData(int id)
        {
            List<UosoConditions> _Inject = new List<UosoConditions>() {
                new UosoConditions(){
                    Key="ParentId",
                    Operator=UosoOperatorEnum.Equal,
                    Value = id,
                    ValueType ="int"
                },
                new UosoConditions(){
                    Key="CreateTime",
                    Operator=UosoOperatorEnum.Less,
                    Value=Convert.ToDateTime("2018-10-19 14:59:57.000000"),
                    ValueType="datetime"

                }
            };
            //var _AllData = _bussCodeInfo.List(z => z.ParentId == id).ToList();
            int alc = 0;
            var _AllData = _bussCodeInfo.GetPage(x => x.Deleted == false, 1, 10, out alc, "Id", "acs").ToList();
            //for (int i = 0; i < 5; i++)
            //    _AllData.AddRange(_AllData);
            var _Re = new LayGridData()
            {
                code = 0,
                count = _AllData.Count() + 10,
                data = _AllData
            };
            return Json(_Re);
        }

        [HttpGet]
        public IActionResult Add(int id)
        {
            if (id == 0)
            {
                return View(new BeCore.Core.Model.BussCode());

            }
            else
            {
                var _Model = _bussCode.List(x => x.Id == id).FirstOrDefault();
                return View(_Model);
            }
        }

        [HttpPost]
        public IActionResult Add(BeCore.Core.Model.BussCode model)
        {
            if (model.Id == 0)//添加
                _bussCode.Add(model);
            else
                _bussCode.Update(model);
            _bussCode.Commit();
            return Json("Fine");
        }
        [HttpGet]
        public IActionResult AddInfo(int id, int parentid)
        {
            if (id == 0)
            {
                return View(new BeCore.Core.Model.BussCodeInfo() { ParentId = parentid });
            }
            else
            {
                return View(_bussCodeInfo.List(z => z.Id == id).FirstOrDefault());
            }
        }

        public IActionResult AddInfo(BeCore.Core.Model.BussCodeInfo model)
        {
            if (model.Id == 0)
                _bussCodeInfo.Add(model);
            else
                _bussCodeInfo.Update(model);
            _bussCodeInfo.Commit();
            return Json("Fine");
        }

        public IActionResult DelInfo(int id)
        {
            _bussCodeInfo.DeleteWhere(x => x.Id == id, true);
            return Json("ok");
        }
    }
}