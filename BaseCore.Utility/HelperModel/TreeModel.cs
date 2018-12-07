using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCore.Utility.HelperModel
{
    public class TreeModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool open { get; set; } = true;
        public bool @checked { get; set; }
        public object children { get; set; }
        public string alias { get; set; }

    }



    public class dropModel
    {

    }
}
