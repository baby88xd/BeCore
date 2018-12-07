using BeCore.Core.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCore.Infrastructure.EntityConfigurations
{
    public class BussCodeInfoConfiguration : EntityBaseConfiguration<BussCodeInfo>
    {
        public override void ConfigureDerived(EntityTypeBuilder<BussCodeInfo> b)
        {
            b.Property(x => x.Deleted).IsRequired();
            //b.Property(x => x.UserGuid).IsRequired();
        }
    }
}
