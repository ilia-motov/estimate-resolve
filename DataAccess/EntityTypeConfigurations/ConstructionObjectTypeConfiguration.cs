using EstimateResolve.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstimateResolve.DataAccess.EntityTypeConfigurations
{
    public class ConstructionObjectTypeConfiguration : IEntityTypeConfiguration<ConstructionObject>
    {
        public void Configure(EntityTypeBuilder<ConstructionObject> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ClientId).IsRequired();
            builder.Property(x => x.Name).IsRequired();

            builder
                .HasOne(x => x.Client)
                .WithMany(x => x.ConstructionObjects)
                .HasForeignKey(x => x.Id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(x => x.Estimates)
                .WithOne()
                .HasForeignKey(x => x.Id)
                .IsRequired();

            builder
                .HasMany(x => x.Contracts)
                .WithOne()
                .HasForeignKey(x => x.Id)
                .IsRequired();
        }
    }
}
