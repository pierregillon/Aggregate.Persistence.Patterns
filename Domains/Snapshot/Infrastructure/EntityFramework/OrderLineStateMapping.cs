﻿using System.Data.Entity.ModelConfiguration;
using Domains.Snapshot.Domain;

namespace Domains.Snapshot.Infrastructure.EntityFramework
{
    public class OrderLineStateMapping : EntityTypeConfiguration<OrderLineState>
    {
        public OrderLineStateMapping()
        {
            this.ToTable("OrderLine");
            this.HasKey(x => new {x.OrderId, x.Product});
            this.Property(x => x.OrderId);
            this.Property(x => x.Product);
            this.Property(x => x.Quantity);
        }
    }
}