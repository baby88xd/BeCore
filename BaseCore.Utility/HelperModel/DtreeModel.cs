using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCore.Utility.HelperModel
{
    public class DtreeModel
    {
        public statusModel status { get; set; }
        public object data { get; set; }
    }

    public class statusModel
    {
        public object code { get; set; }
        public string message { get; set; }
    }

    public class DataLs
    {
        public string id { get; set; }
        public string title { get; set; }
        public bool isLast { get; set; }
        public string level { get; set; }
        public string parentId { get; set; }
        public object checkArr { get; set; }
        public object children { get; set; }

    }


    public class checkArr
    {
        public string type { get; set; }
        public string isChecked { get; set; }
    }
}
