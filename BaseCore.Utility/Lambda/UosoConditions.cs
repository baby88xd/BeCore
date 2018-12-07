using System;
using System.Collections.Generic;
using System.Text;

namespace System.Linq.Expressions
{
    public class UosoConditions
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }
        /// <summary>
        /// 值类型
        /// </summary>
        public string ValueType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UosoOperatorEnum Operator { get; set; }

        public string OperatorMethod { get; set; }
    }
    /// <summary>
    /// Lambda方法拼接地址
    /// </summary>
    public enum UosoOperatorEnum
    {
        Contains,
        Equal,
        Greater,
        GreaterThan,
        GreaterEqual,
        Less,
        LessEqual,
        NotEqual,
        In,
        Between
    }

}
