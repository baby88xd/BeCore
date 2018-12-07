using System.Linq;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace System.Linq.Expressions
{
    public class SearchModel
    {
        /// <summary>
        /// 第几页
        /// </summary>
        public int page { get; set; }
        /// <summary>
        /// 显示多少行
        /// </summary>
        public int limit { get; set; }
        /// <summary>
        /// 排序字段名称
        /// </summary>
        public string field { get; set; }
        /// <summary>
        /// 排序方式
        /// </summary>
        public string order { get; set; }
        /// <summary>
        /// 查询条件
        /// </summary>
        public string Conditions { get; set; }

        private List<UosoConditions> _whereLambda;
        /// <summary>
        /// 查询条件
        /// </summary>
        public List<UosoConditions> WhereLambda
        {
            get => (_whereLambda != null) ? _whereLambda : (string.IsNullOrEmpty(Conditions) ? new List<UosoConditions>() : Conditions.JsonStringToList<UosoConditions>());
            set => _whereLambda = value;
        }// => string.IsNullOrEmpty(Conditions) ? new List<UosoConditions>() : Conditions.JsonStringToList<UosoConditions>();



    }
}
