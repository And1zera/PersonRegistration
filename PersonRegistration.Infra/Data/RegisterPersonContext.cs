using Microsoft.EntityFrameworkCore;
using PersonRegistration.Infra.Configurations;

namespace PersonRegistration.Infra.Data
{
    public class RegisterPersonContext : DbContext
    {
        public RegisterPersonContext(DbContextOptions<RegisterPersonContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
        }
    }
}
