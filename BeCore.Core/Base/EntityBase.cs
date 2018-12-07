using System;
using System.Collections.Generic;
using System.Text;

namespace BeCore.Core.Base
{
    public abstract class EntityBase
    {
        //自增长逐渐
        public int Id { get; set; }
        //是否删除 今后肯定要软删除的
        public bool Deleted { get; set; }
        //创建时间
        public DateTime CreateTime { get; set; }
        //删除时间
        public DateTime? DeleteTime { get; set; }

    }
}
