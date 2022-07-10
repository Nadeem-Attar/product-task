using Microsoft.EntityFrameworkCore;
using ProductsTask.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsTask.SqlServer.Database
{
    public class PTDbContext : DbContext
    {
        public PTDbContext(DbContextOptions<PTDbContext> options)
            : base(options) { }

        #region Core Tables
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        #endregion


        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
