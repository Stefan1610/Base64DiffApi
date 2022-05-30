using Descarta2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;

namespace Descarta2.Context
{
    public class DiffContext : DbContext
    {
        public DiffContext(DbContextOptions<DiffContext> options):base(options)
        {
            var builder = new DbContextOptionsBuilder<DiffContext>();
        builder.UseInMemoryDatabase(databaseName: "DiffData");
            options = builder.Options;
        }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "DiffData");
        }

        public DbSet<DiffItem> DiffItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DiffItem>().HasKey(table => new { table.Id, table.Position });
        }
    }
}
