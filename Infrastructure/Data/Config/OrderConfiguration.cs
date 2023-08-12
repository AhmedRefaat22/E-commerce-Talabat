using Core.Entities.OrderAggreagate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(order => order.ShippedToAddress, a => a.WithOwner());

            builder.Property(order => order.OrderStatus)
                .HasConversion(
                    status => status.ToString(),
                    value => (OrderStatus)Enum.Parse(typeof(OrderStatus), value)
                );

            builder.HasMany(order => order.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
