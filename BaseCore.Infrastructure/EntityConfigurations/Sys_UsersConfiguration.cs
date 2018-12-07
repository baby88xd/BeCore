using BeCore.Core.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCore.Infrastructure.EntityConfigurations
{
    public class Sys_UsersConfiguration : EntityBaseConfiguration<Sys_Users>
    {
        public override void ConfigureDerived(EntityTypeBuilder<Sys_Users> b)
        {
            b.Property(x => x.Deleted).IsRequired();
        }
    }
}
