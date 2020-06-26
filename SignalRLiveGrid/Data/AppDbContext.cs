using Microsoft.EntityFrameworkCore;

namespace SignalRLiveGrid.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public AppDbContext()
        {

        }

        public virtual DbSet<Person> People { get; set; }

        #region Seed DataBase

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Person>().HasData(
                new Person { PersonId = 1, Name = "John", IsLock = false, Salary = 10 },
                new Person { PersonId = 2, Name = "Logan", IsLock = false, Salary = 20 },
                new Person { PersonId = 3, Name = "James", IsLock = false, Salary = 30 }
            );
        }


        #endregion
    }
}