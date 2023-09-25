using Microsoft.EntityFrameworkCore;
using TestAPI.Models;

namespace TestAPI.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Counter> Counters { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> opt) : base(opt){}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=CountersDB;Username=postgres;Password=testpass");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Counter>().HasData(
                new Counter { Id = 1, Key = 1, Value = 1 },
                new Counter { Id = 2, Key = 1, Value = 2 },
                new Counter { Id = 3, Key = 1, Value = 3 },
                new Counter { Id = 4, Key = 2, Value = 1 },
                new Counter { Id = 5, Key = 2, Value = 1 },
                new Counter { Id = 6, Key = 2, Value = 3 },
                new Counter { Id = 7, Key = 2, Value = 1 }
                );
        }
    }
}
