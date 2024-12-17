using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;
using samp.Models;

namespace samp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
 
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Meeting> Meeting { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        //public DbSet<Holiday> HomePageViewModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Emp id as the primary key
            modelBuilder.Entity<Employees>()
                .HasKey(e => e.EmployeeId);
        }
    }
}
