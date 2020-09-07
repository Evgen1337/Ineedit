using Ads.Domain.AggregatesModel.AdAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ads.Infrastructure.EntityConfigurations
{
    public class AdEntityTypeConfiguration : IEntityTypeConfiguration<Ad>
    {
        public void Configure(EntityTypeBuilder<Ad> builder)
        {
            builder.ToTable("Ads", AdsContext.DefaultSchema);
            builder.HasKey(o => o.Id);

            builder.Property(p => p.OwnerId)
                .HasColumnName("OwnerId")
                .IsRequired();

            builder.Property(p => p.Name)
                .HasColumnName("Name")
                .IsRequired();

            builder.Property(p => p.CreationDate)
               .HasColumnName("CreationDate")
               .IsRequired();

            builder.Property(p => p.Comment)
               .HasColumnName("Comment")
               .IsRequired(false);

            builder.Property<int>("AdTypeId")
                .HasColumnName("AdTypeId")
                .IsRequired();

            builder.HasOne(p => p.AdType)
                .WithMany()
                .HasForeignKey("AdTypeId");
        }
    }
}
