using BeCore.Core.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCore.Infrastructure.EntityConfigurations
{
    public class Sys_DepartmentsConfiguration : EntityBaseConfiguration<Sys_Departments>
    {
        public override void ConfigureDerived(EntityTypeBuilder<Sys_Departments> b)
        {
            b.Property(x => x.Deleted).IsRequired();
        }
    }
}
