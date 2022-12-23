using EstimateResolve.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EstimateResolve.DataAccess.EntityTypeConfigurations
{
    public class ContractTypeConfiguration : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ClientId).IsRequired();
            builder.Property(x => x.ConstructionObject).IsRequired();
            builder.Property(x => x.Name).IsRequired();

            builder
                .HasOne(x => x.Client)
                .WithMany(x => x.Contracts)
                .HasForeignKey(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder
               .HasOne(x => x.ConstructionObject)
               .WithMany(x => x.Contracts)
               .HasForeignKey(x => x.Id)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(x => x.Estimates)
                .WithOne()
                .HasForeignKey(x => x.Id)
                .IsRequired();
        }
    }
}
