using EstimateResolve.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EstimateResolve.DataAccess.EntityTypeConfigurations
{
    public class EstimateMaterialEntityTypeConfiguration : IEntityTypeConfiguration<EstimateMaterial>
    {
        public void Configure(EntityTypeBuilder<EstimateMaterial> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.EstimateId).IsRequired();
            builder.Property(x => x.EstimateWorkId).IsRequired();
            builder.Property(x => x.MaterialId).IsRequired();
            builder.Property(x => x.Consumption).HasPrecision(15, 2).IsRequired();
            builder.Property(x => x.ValueWorking).HasPrecision(15, 2).IsRequired();
            builder.Property(x => x.Quantity).HasPrecision(15, 3).IsRequired();
            builder.Property(x => x.Price).HasPrecision(15, 2);
            builder.Property(x => x.Amount).HasPrecision(15, 2);

            builder
                .HasOne(x => x.Estimate)
                .WithMany(x => x.EstimateMaterials)
                .HasForeignKey(x => x.Id)
                .IsRequired();

            builder
                .HasOne(x => x.EstimateWork)
                .WithMany(x => x.EstimateMaterials)
                .HasForeignKey(x => x.Id)
                .IsRequired();

            builder
                .HasOne(x => x.Material)
                .WithMany(x => x.EstimateMaterials)
                .HasForeignKey(x => x.Id)
                .IsRequired();

        }
    }
}
