using System;
using System.Collections.Generic;
using System.Text;
using EShop.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EShop.Data.Context
{
    public class EShopContext : DbContext
    {
        public EShopContext(DbContextOptions<EShopContext> options) : base(options)
        {

        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<BlogAuthor> BlogAuthors { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogCategory>()
                .HasOne(o => o.Category)
                .WithMany(o => o.BlogCategories)
                .HasForeignKey(f => f.ParentId);
        }
    }
}
