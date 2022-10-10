using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microservice.Core.Domain;
using Microservice.Infrastructure.Context;

namespace Microservice.Infrastructure.EntityConfiguration
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("siparisler");
            builder.Property(oi=>oi.Id).UseHiLo("order_hilo").IsRequired();
            builder.Property(oi => oi.Id).IsRequired();
            builder.Property(oi => oi.Id).IsRequired();
            builder.Property(oi => oi.Price).IsRequired();
            builder.Property(oi => oi.BrandId).IsRequired();
            builder.Property(oi => oi.StoreName).IsRequired(false);
            builder.Property(oi => oi.CustomerName).IsRequired(false);
            builder.Property(oi => oi.CreatedOn).IsRequired();
            builder.Property(oi => oi.Status).IsRequired();


        }
    }
}
