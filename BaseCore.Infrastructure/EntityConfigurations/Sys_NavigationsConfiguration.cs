using BeCore.Core.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCore.Infrastructure.EntityConfigurations
{
    public class Sys_NavigationsConfiguration : EntityBaseConfiguration<Sys_Navigations>
    {
        public override void ConfigureDerived(EntityTypeBuilder<Sys_Navigations> b)
        {
            b.Property(x => x.Deleted).IsRequired();
            //b.Property(x => x.UserGuid).IsRequired();
        }
    }
}
