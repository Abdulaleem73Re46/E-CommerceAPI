

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities;
namespace Configurations;


public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>

{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
       builder.HasKey(o=>o.OrderItemId);
       builder.Property(o=>o.PriceAtPurchase).HasColumnType("decimal(10,2)");
       builder.Property(o=>o.Quantity).HasColumnType("INTEGER");
       builder.HasOne(o=>o.Order)
       .WithMany(o=>o.OrderItems)
       .HasForeignKey(o=>o.OrderId)
       .OnDelete(DeleteBehavior.Cascade);
       




    }
}