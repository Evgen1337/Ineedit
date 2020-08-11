using Ads.Domain.AggregatesModel.AdAggregate;
using Ads.Domain.SeedWork;
using Ads.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Ads.Infrastructure
{
    public class AdsContext : DbContext, IUnitOfWork
    {
        public const string DefaultSchema = "dbo";

        public DbSet<Ad> Ads { get; set; }
        public DbSet<AdType> AdTypes { get; set; }

        public AdsContext(DbContextOptions<AdsContext> options) :
            base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AdEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AdTypeEntityTypeConfiguration());
        }
    }

    public class AdsContextDesignFactory : IDesignTimeDbContextFactory<AdsContext>
    {
        public AdsContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AdsContext>()
                .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Ineedit.Ads;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            return new AdsContext(optionsBuilder.Options);
        }
    }
}
