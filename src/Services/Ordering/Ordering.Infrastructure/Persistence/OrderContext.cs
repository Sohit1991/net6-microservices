using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Common;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContext : DbContext
    {
        //private readonly DbContextOptions<OrderContext> options;

        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
            //this.options = options;
        }
        public DbSet<Order> Orders { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entity in ChangeTracker.Entries<EntityBase>()) 
            {
                switch (entity.State)
                {                    
                    case EntityState.Modified:
                        entity.Entity.LastModifiedDate = DateTime.Now;
                        entity.Entity.LastModifiedBy = "sohit";
                        break;
                    case EntityState.Added:
                        entity.Entity.CreatedDate = DateTime.Now;
                        entity.Entity.CreatedBy = "sohit";
                        break;
                    
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
