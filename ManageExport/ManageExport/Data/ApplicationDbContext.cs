using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManageExport.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
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

            base.OnModelCreating(builder);
            builder.Entity<ProductCategory>().HasKey(sc => new { sc.CategoryId, sc.ProductId });
            //builder.Entity<ExportListDetail>().HasKey(sc => new { sc.ExportDocumentBillId, sc.ProductId});

        }
    }
}
