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

    }
}
