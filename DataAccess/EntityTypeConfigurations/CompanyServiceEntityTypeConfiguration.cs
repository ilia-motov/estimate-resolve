using EstimateResolve.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstimateResolve.DataAccess.EntityTypeConfigurations
{
    public class CompanyServiceEntityTypeConfiguration : IEntityTypeConfiguration<CompanyService>
    {
        public void Configure(EntityTypeBuilder<CompanyService> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.UnitOfMeasurementId).IsRequired();
            builder.Property(x => x.Price).IsRequired();

            builder
                .HasOne(x => x.UnitOfMeasurement)
                .WithMany(x => x.CompanyServices)
                .HasForeignKey(x => x.Id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(x => x.EstimateWorks)
                .WithOne()
                .HasForeignKey(x => x.Id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
