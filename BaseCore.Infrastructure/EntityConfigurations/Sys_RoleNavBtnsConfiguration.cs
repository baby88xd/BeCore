using BeCore.Core.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCore.Infrastructure.EntityConfigurations
{
    public class Sys_RoleNavBtnsConfiguration : EntityBaseConfiguration<Sys_RoleNavBtns>
    {
        public override void ConfigureDerived(EntityTypeBuilder<Sys_RoleNavBtns> b)
        {
            b.Property(x => x.Deleted).IsRequired();
            //b.Property(x => x.UserGuid).IsRequired();
        }
    }
    //class Sys_RoleNavBtnsConfiguration
    //{
    //}
}
