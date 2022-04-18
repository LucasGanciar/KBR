using KBR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBR.Domain.Infra
{
    public class dbContext : DbContext
    {
        public dbContext(DbContextOptions<dbContext> options) : base(options)
        {

        }

        public DbSet<Address> addresses { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderItem> orderItems { get; set; }
        public DbSet<OrderStatus> orderStatus { get; set; }
        public DbSet<Payment> payments { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<ProductType> productTypes { get; set; }

    }
}
