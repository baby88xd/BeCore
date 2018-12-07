using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCore.Utility.HelperModel
{
    public class NavModel
    {
        public string msg { get; set; }
        public int code { get; set; }
        public object data { get; set; }

        public int count { get; set; }
        public bool @is { get; set; }
        public string tip { get; set; }

        //"count": 924,
        //"is": true,
        //"tip": "操作成功！"
    }
}
