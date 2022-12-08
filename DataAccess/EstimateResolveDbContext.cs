using System.Globalization;
using EstimateResolve.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EstimateResolve.DataAccess
{
    public class EstimateResolveDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public EstimateResolveDbContext(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

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
            optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));

            var fileBasedDatabase = _configuration.GetValue<bool>("IsFileBasedDatabase");
            if (fileBasedDatabase)
            {
                var connectionString = _configuration.GetConnectionString("FileBasedConnection");
                var connection = new SqliteConnection(connectionString);

                connection.CreateCollation("NOCASE", (x, y) => string.Compare(x, y, true, new CultureInfo("ru-RU")));
                connection.CreateFunction("upper", (string value) => value.ToUpper(new CultureInfo("ru-RU")));
                connection.CreateFunction("lower", (string value) => value.ToLower(new CultureInfo("ru-RU")));

                optionsBuilder.UseSqlite(connection)
                    .UseSnakeCaseNamingConvention();
            }
            else
            {
                optionsBuilder
                    .UseNpgsql(_configuration.GetConnectionString("ServerBasedConnection"))
                    .UseSnakeCaseNamingConvention();
            }
        }
    }
}
