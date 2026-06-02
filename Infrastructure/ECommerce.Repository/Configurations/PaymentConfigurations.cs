// Infrastructure/Configurations/PaymentConfiguration.cs
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(p => p.PaymentId);
        
        // builder.HasOne(p => p.Order)
        //     .WithOne(o => o.Payment);
            



        builder.Property(p=>p.PayMethod).HasConversion<string>();
        builder.Property(p=>p.PayStatus).HasConversion<string>();    
    
    builder.HasOne(p=>p.Order)
    .WithMany(o=>o.Payments).HasForeignKey(p=>p.OrderId).OnDelete(DeleteBehavior.Restrict);
    
    }
}