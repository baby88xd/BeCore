using BeCore.Core.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCore.Infrastructure.EntityConfigurations
{
    public class BussCodeConfiguration : EntityBaseConfiguration<BussCode>
    {
        public override void ConfigureDerived(EntityTypeBuilder<BussCode> b)
        {
            b.Property(x => x.Deleted).IsRequired();
            //b.Property(x => x.UserGuid).IsRequired();
        }
    }
}
