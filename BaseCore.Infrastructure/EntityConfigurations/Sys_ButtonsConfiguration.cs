using BeCore.Core.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCore.Infrastructure.EntityConfigurations
{

    public class Sys_ButtonsConfiguration : EntityBaseConfiguration<Sys_Buttons>
    {
        public override void ConfigureDerived(EntityTypeBuilder<Sys_Buttons> b)
        {
            b.Property(x => x.Deleted).IsRequired();
            //b.Property(x => x.UserGuid).IsRequired();
        }
    }
}
