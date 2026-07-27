using System;
using System.Collections.Generic;
using System.Text;
using EFCORE_02_task.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCORE_02_task
{
    public class ProjectContext : DbContext
    {
        public DbSet<Review> reviews {  get; set; }
        public DbSet<Order> orders { get; set; }
         public DbSet<Product> products { get; set; }
         public DbSet<Category> categories { get; set; }
         public DbSet<User> users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
              "Server=(localdb)\\MSSQLLocalDB;Database=EFCORE_02_Task_DB;Trusted_Connection=True;TrustServerCertificate=True;");
        }






    }
}
