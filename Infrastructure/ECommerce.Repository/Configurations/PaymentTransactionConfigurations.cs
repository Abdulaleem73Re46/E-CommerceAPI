using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Configurations;
public class PaymentTransactionConfiguration : IEntityTypeConfiguration<PaymentTransaction>
{
public void Configure(EntityTypeBuilder<PaymentTransaction> builder)
{
builder.HasKey(t => t.Id);
builder.Property(t => t.Amount).HasColumnType("decimal(18,2)").IsRequired();
builder.Property(pt=>pt.type).HasConversion<string>();
builder.HasOne(t => t.Payment)
.WithMany(p => p.Transactions)
.HasForeignKey(t => t.PaymentId)
.OnDelete(DeleteBehavior.Cascade);
}}