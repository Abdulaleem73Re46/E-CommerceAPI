

using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Configurations;

    public class CategoryConfigurations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // Configure the Category entity
            builder.HasKey(c=>c.CategoryId);
           
            builder.Property(c=>c.Name).HasMaxLength(30);
   

            // Configure the relationship with Order
            builder.HasMany(c=>c.Products)
                   .WithOne(p=>p.Category)
                   .HasForeignKey(p=>p.CategoryId);
        }
    }
