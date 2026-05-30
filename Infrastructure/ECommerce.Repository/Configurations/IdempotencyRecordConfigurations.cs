using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Configurations;




public class IdempotencyRecordConfiguration : IEntityTypeConfiguration<IdempotencyRecord>
{
public void Configure(EntityTypeBuilder<IdempotencyRecord> builder)
{
builder.HasKey(i => i.Id);
builder.Property(i => i.Key).HasMaxLength(100).IsRequired();
builder.HasIndex(i => i.Key).IsUnique();
}
}