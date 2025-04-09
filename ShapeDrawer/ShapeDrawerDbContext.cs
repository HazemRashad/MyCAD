using System.Data.Entity;

namespace ShapeDrawer.Models
{
    public class ShapeDrawerDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Shape> Shapes { get; set; }

        public ShapeDrawerDbContext() : base("name=ShapeDrawerDbConnectionString")
        {
        }
    }
}