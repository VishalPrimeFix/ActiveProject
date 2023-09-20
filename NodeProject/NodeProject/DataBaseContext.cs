using Microsoft.EntityFrameworkCore;
using NodeProject.Models;

namespace NodeProject
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }

        public DbSet<Node> Nodes { get; set; }

        // Add DbSet properties for other entities here if needed

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure entity relationships, constraints, and more here if needed
        }
    }
}
