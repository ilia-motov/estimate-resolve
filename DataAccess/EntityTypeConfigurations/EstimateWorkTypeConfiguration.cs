using EstimateResolve.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EstimateResolve.DataAccess.EntityTypeConfigurations
{
    public class EstimateWorkTypeConfiguration : IEntityTypeConfiguration<EstimateWork>
    {
        public void Configure(EntityTypeBuilder<EstimateWork> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.EstimateId).IsRequired();
            builder.Property(x => x.CompanyServiceId).IsRequired();
            builder.Property(x => x.Value).HasPrecision(15, 2).IsRequired();
            builder.Property(x => x.Price).HasPrecision(15, 2);
            builder.Property(x => x.Amount).HasPrecision(15, 2);

            builder
                .HasOne(x => x.Estimate)
                .WithMany(x => x.EstimateWorks)
                .HasForeignKey(x => x.Id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.CompanyService)
                .WithMany(x => x.EstimateWorks)
                .HasForeignKey(x => x.Id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(x => x.EstimateMaterials)
                .WithOne()
                .HasForeignKey(x => x.Id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
