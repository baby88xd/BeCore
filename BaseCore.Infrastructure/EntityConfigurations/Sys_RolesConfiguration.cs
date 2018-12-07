using BeCore.Core.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCore.Infrastructure.EntityConfigurations
{
    //class Sys_RolesConfiguration
    //{
    //}
    public class Sys_RolesConfiguration : EntityBaseConfiguration<Sys_Roles>
    {
        public override void ConfigureDerived(EntityTypeBuilder<Sys_Roles> b)
        {
            b.Property(x => x.Deleted).IsRequired();
            //b.Property(x => x.UserGuid).IsRequired();
        }
    }
}
