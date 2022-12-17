using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCShop.Models;

namespace MVCShop.Data
{
    public class MVCShopContext : DbContext
    {
        public MVCShopContext (DbContextOptions<MVCShopContext> options)
            : base(options)
        {
        }

        public DbSet<MVCShop.Models.Category> Category { get; set; } = default!;

        public DbSet<MVCShop.Models.Customer> Customer { get; set; } = default!;

        public DbSet<MVCShop.Models.Product> Product { get; set; } = default!;

        public DbSet<MVCShop.Models.Order> Order { get; set; } = default!;

        public DbSet<MVCShop.Models.OrderDetail> OrderDetail { get; set; } = default!;
    }
}
