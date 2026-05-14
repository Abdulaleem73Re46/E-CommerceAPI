using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Infrastructure.Data;
namespace Repository;


public class RepositoryContext : IdentityDbContext<User>

{
    public RepositoryContext(DbContextOptions options) : base(options)
    {
    }
    
    // Add your DbSets for other entities
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    

    
     protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RepositoryContext).Assembly);   
        // Apply seed data
       // modelBuilder.Seed();
    }
}