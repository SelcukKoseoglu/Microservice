using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microservice.Core.Domain;
using Microservice.Infrastructure.EntityConfiguration;
using Microservice;


namespace Microservice.Infrastructure.Context
{
    public class OrderContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "order";
        
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
            

        }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new OrderEntityTypeConfiguration());
            

        }

    }
}
