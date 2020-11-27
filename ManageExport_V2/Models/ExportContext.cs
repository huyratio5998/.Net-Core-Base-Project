using ManageExport_V2.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageExport_V2.Models
{
    public class ExportContext : DbContext
    {
        public ExportContext(DbContextOptions<ExportContext> options)
           : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<ExportListDetail> ExportListDetails { get; set; }
        public DbSet<ExportDocumentBill> ExportDocumentBills { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // create table name by string.
            builder.Entity<Product>(entity => { entity.ToTable("Product"); });
            builder.Entity<ProductCategory>(entity => { entity.ToTable("ProductCategory"); });
            builder.Entity<Category>(entity => { entity.ToTable("Category"); });
            builder.Entity<Stock>(entity => { entity.ToTable("Stock"); });
            builder.Entity<ExportListDetail>(entity => { entity.ToTable("ExportListDetail"); });
            builder.Entity<ExportDocumentBill>(entity => { entity.ToTable("ExportDocumentBill"); });
            builder.Entity<Image>(entity => { entity.ToTable("Image"); });
            builder.Entity<Brand>(entity => { entity.ToTable("Brand"); });
            builder.Entity<User>(entity => { entity.ToTable("User"); });

            base.OnModelCreating(builder);
            builder.Entity<ProductCategory>().HasKey(sc => new { sc.CategoryId, sc.ProductId });
            //builder.Entity<ExportListDetail>().HasKey(sc => new { sc.ExportDocumentBillId, sc.ProductId});

        }
    }
}
