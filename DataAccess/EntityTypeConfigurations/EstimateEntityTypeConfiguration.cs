using EstimateResolve.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstimateResolve.DataAccess.EntityTypeConfigurations
{
    public class EstimateEntityTypeConfiguration : IEntityTypeConfiguration<Estimate>
    {
        public void Configure(EntityTypeBuilder<Estimate> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Number).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.DevelopmentDate).IsRequired();
            builder.Property(x => x.ConstructionObjectId).IsRequired();
            builder.Property(x => x.ClientId).IsRequired();
            builder.Property(x => x.ContractId).IsRequired();

            builder
                .HasOne(x => x.ConstructionObject)
                .WithMany(x => x.Estimates)
                .HasForeignKey(x => x.Id)
                .IsRequired();

            builder
                .HasOne(x => x.Client)
                .WithMany(x => x.Estimates)
                .HasForeignKey(x => x.Id)
                .IsRequired();

            builder
                .HasOne(x => x.Contract)
                .WithMany(x => x.Estimates)
                .HasForeignKey(x => x.Id)
                .IsRequired();

            builder
                .HasMany(x => x.EstimateMaterials)
                .WithOne()
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
