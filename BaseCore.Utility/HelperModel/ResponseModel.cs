using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BaseCore.Utility.HelperModel
{
    public class ResponseModel
    {
        /// <summary>
        /// 跳转url
        /// </summary>
        public string redirectUrl { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public object Data { get; set; }

    }


    public enum httpStatu
    {
        [Description("成功")]
        Success,
        [Description("失败")]
        Error,
        Fail
    }
}
