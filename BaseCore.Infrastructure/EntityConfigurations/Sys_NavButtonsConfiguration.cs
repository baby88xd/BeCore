using BeCore.Core.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCore.Infrastructure.EntityConfigurations
{
    public class Sys_NavButtonsConfiguration : EntityBaseConfiguration<Sys_NavButtons>
    {
        public override void ConfigureDerived(EntityTypeBuilder<Sys_NavButtons> b)
        {
            b.Property(x => x.Deleted).IsRequired();
        }
    }
}
