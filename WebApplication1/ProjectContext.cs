using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1
{
    public class ProjectContext : DbContext
    {
        public DbSet<Product> _products {  get; set; }
        public DbSet<Category> _categories { get; set; }

        public ProjectContext(
            DbContextOptions<ProjectContext> options)
            : base(options)
                  {

                  }
    }
}
