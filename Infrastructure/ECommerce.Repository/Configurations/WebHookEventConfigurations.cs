
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Configurations;




public class WebHookEventConfiguration : IEntityTypeConfiguration<WebHookEvent>
{

    public void Configure(EntityTypeBuilder<WebHookEvent> builder)
    {
        builder.HasKey(w => w.Id);
builder.Property(w => w.EventId).HasMaxLength(100).IsRequired();
builder.HasIndex(w => w.EventId).IsUnique();
    }
}
