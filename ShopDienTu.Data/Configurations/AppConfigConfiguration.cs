using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopDienTu.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopDienTu.Data.Configurations
{
    public class AppConfigConfiguration : IEntityTypeConfiguration<AppConfig>
    {
        public void Configure(EntityTypeBuilder<AppConfig> builder)
        {
            builder.ToTable("AppConfig").HasKey(x => x.Key);
            builder.Property(x => x.Value).IsRequired();
        }
    }
}
