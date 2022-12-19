using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassTransitHelper.Saga.Persistance
{
    public class OrderStateConfiguration :
            IEntityTypeConfiguration<OrderState>
    {
        public void Configure(EntityTypeBuilder<OrderState> builder)
        {
            builder.Property(x => x.CurrentState).HasMaxLength(64);
            builder.Property(x => x.OrderDate);

        }
    }
}
