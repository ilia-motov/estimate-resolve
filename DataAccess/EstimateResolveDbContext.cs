using EstimateResolve.Entities;
using Microsoft.EntityFrameworkCore;

namespace EstimateResolve.DataAccess
{
    public class EstimateResolveDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }

        public DbSet<CompanyService> CompanyServices { get; set; }

        public DbSet<ConstructionObject> ConstructionObjects { get; set; }

        public DbSet<Estimate> Estimates { get; set; }

        public DbSet<EstimateMaterial> EstimateMaterials { get; set; }

        public DbSet<EstimateWork> EstimateWorks { get; set; }

        public DbSet<Material> Materials { get; set; }

        public DbSet<UnitOfMeasurement> UnitOfMeasurements { get; set; }

        public DbSet<Contract> Contracts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlite("Filename=EstimateResolve.db")
                .UseSnakeCaseNamingConvention();

            return;

            optionsBuilder
                .UseNpgsql("Host=localhost:5432;Database=Estimate;Username=posgres;Password=admin")
                .UseSnakeCaseNamingConvention();
        }
    }
}
