using Microsoft.EntityFrameworkCore;
using Minsait.Examples.Domain.Entities;

namespace Minsait.Examples.Infra.Data.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        public DbSet<MinsaitTest> Tests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MinsaitTest>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<MinsaitTest>()
                .Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<MinsaitTest>()
                .Property(t => t.Active)
                .IsRequired();
        }
    }
}
