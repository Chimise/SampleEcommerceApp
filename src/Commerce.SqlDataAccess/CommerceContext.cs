using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Commerce.Domain.AuditEntries;
using Commerce.Domain.Orders;
using Commerce.Domain.Products;
using Commerce.Domain.ProductInventories;

namespace Commerce.SqlDataAccess
{
    public class CommerceContext : DbContext
    {
        public CommerceContext(DbContextOptions<CommerceContext> options) : base(options)
        {
           
        }

        public DbSet<Order> Orders {  get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductInventory> ProductInventories { get; set; }
        public DbSet<AuditEntry> AuditEntries {  get; set; }

        public bool IsNew<TEntity>(TEntity entity) where TEntity : class
        {
            return !this.Set<TEntity>().Local.Any(e => e == entity);
        }
    }
}
