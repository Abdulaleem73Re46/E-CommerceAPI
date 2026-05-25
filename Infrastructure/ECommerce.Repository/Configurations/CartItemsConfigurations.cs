

using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Configurations;


public class CartItemConfigurations : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.ToTable("CartItems");
        builder.HasKey(ct=>ct.Id);
        builder.Property(c=>c.Quantity).IsRequired().HasDefaultValue(1);
    builder.HasIndex(ci=>new{ci.CartId,ci.ProductId});


    builder.HasOne(c=>c.Product)
    .WithMany(p=>p.CartItems)
    .HasForeignKey(ci=>ci.ProductId)
    .OnDelete(DeleteBehavior.Restrict);
   
builder.HasOne(ci=>ci.Cart)
.WithMany(c=>c.CartItems)
.HasForeignKey(ci=>ci.CartId);

    }
}