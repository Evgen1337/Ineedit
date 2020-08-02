using Ads.Domain.AggregatesModel.AdAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ads.Infrastructure.EntityConfigurations
{
    public class AdTypeEntityTypeConfiguration : IEntityTypeConfiguration<AdType>
    {
        public void Configure(EntityTypeBuilder<AdType> builder)
        {
            builder.ToTable("AdTypes", AdsContext.DefaultSchema);

            builder.HasKey(o => o.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(p => p.Name)
                .HasColumnName("Name")
                .IsRequired();
        }
    }
}
