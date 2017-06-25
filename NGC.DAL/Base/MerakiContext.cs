using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace NGC.DAL.Base
{
    public class MerakiContext : DbContext
    {
        protected string ConnectionString { get; private set; }

        public MerakiContext(IOptions<MerakiConfiguration> settings)
        {
            this.ConnectionString = settings.Value.ConnectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(ConnectionString,(builder)=> 
                builder.MigrationsHistoryTable("ngc_migrations")
                
            );
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            MerakiEntityMappings.MapUser(modelBuilder);
            MerakiEntityMappings.MapCustomer(modelBuilder);
            MerakiEntityMappings.MapConfiguration(modelBuilder);
        }
    }
}
