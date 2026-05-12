// using Core.Entities;
// using Core.Enum.OrderStatus;
// using Microsoft.EntityFrameworkCore;

// namespace Infrastructure.Data;

// public static class SeedData
// {
//     public static void Seed(this ModelBuilder modelBuilder)
//     {
//         // Users
//         modelBuilder.Entity<User>().HasData(
//             new User
//             {
//                 Id = "AAAAAAAA-BBBB-CCCC-DDDD-000000000001",
//                 UserName = "john.doe@email.com",
//                 NormalizedUserName = "JOHN.DOE@EMAIL.COM",
//                 Email = "john.doe@email.com",
//                 NormalizedEmail = "JOHN.DOE@EMAIL.COM",
//                 EmailConfirmed = true,
//                 PasswordHash = "AQAAAAIAAYagAAAAEP0Z0XG0bQn1HkxNVwOMj0vFhJYz0iM6w7ZWxHsiZ0XdHj8z0YwVPqP0Km0Pz0Vz0Q==",
//                 SecurityStamp = "SECURITYSTAMP000000000000000000001",
//                 ConcurrencyStamp = "11111111-1111-1111-1111-111111111111",
//                 PhoneNumber = "123-456-7890",
//                 PhoneNumberConfirmed = true,
//                 TwoFactorEnabled = false,
//                 LockoutEnabled = true,
//                 AccessFailedCount = 0,
//                 Address = "123 Main Street, New York, NY 10001",
//                 FullName = "John Doe"
//             },
//             new User
//             {
//                 Id = "AAAAAAAA-BBBB-CCCC-DDDD-000000000002",
//                 UserName = "jane.smith@email.com",
//                 NormalizedUserName = "JANE.SMITH@EMAIL.COM",
//                 Email = "jane.smith@email.com",
//                 NormalizedEmail = "JANE.SMITH@EMAIL.COM",
//                 EmailConfirmed = true,
//                 PasswordHash = "AQAAAAIAAYagAAAAEP0Z0XG0bQn1HkxNVwOMj0vFhJYz0iM6w7ZWxHsiZ0XdHj8z0YwVPqP0Km0Pz0Vz0Q==",
//                 SecurityStamp = "SECURITYSTAMP000000000000000000002",
//                 ConcurrencyStamp = "22222222-2222-2222-2222-222222222222",
//                 PhoneNumber = "098-765-4321",
//                 PhoneNumberConfirmed = true,
//                 TwoFactorEnabled = false,
//                 LockoutEnabled = true,
//                 AccessFailedCount = 0,
//                 Address = "456 Oak Avenue, Los Angeles, CA 90001",
//                 FullName = "Jane Smith"
//             }
//         );

//         // Categories
//         modelBuilder.Entity<Category>().HasData(
//             new Category { CategoryId = Guid.NewGuid(), Name = "Electronics", CreatedAt = DateTime.UtcNow },
//             new Category { CategoryId = Guid.NewGuid(), Name = "Clothing", CreatedAt = DateTime.UtcNow },
//             new Category { CategoryId = Guid.NewGuid(), Name = "Books", CreatedAt = DateTime.UtcNow }
//         );

// private static void SeedOrders(ModelBuilder modelBuilder)
// {
//     var orders = new[]
//     {
//         new Order
//         {
//             OrderId = Guid.Parse("1a2b3c4d-5e6f-4a70-8901-abcdef123456"),
//             UserId = "AAAAAAAA-BBBB-CCCC-DDDD-000000000001",  // ✅ string
//             TotalPrice = 724.98m,
//             OrderDate = new DateTime(2025, 1, 15, 10, 30, 0, DateTimeKind.Utc),
//             Status = OrderStatus.Delivered
//         },
//         new Order
//         {
//             OrderId = Guid.Parse("2b3c4d5e-6f7a-4b80-9012-bcdef1234567"),
//             UserId = "AAAAAAAA-BBBB-CCCC-DDDD-000000000001",  // ✅ string
//             TotalPrice = 1389.98m,
//             OrderDate = new DateTime(2025, 3, 20, 14, 45, 0, DateTimeKind.Utc),
//             Status = OrderStatus.Confirmed
//         }
//     };

//     modelBuilder.Entity<Order>().HasData(orders);
// }


//     }
  //  }





























// using Core.Entities;
// using Core.Enum.OrderStatus;
// using Core.Enum.PaymentMethod;
// using Core.Enum.PaymentStatus;
// using Microsoft.EntityFrameworkCore;

// namespace Infrastructure.Data;

// public static class SeedData
// {
//     // Fix the enum names if needed
//     private static PaymentMethod GetPaymentMethod(PaymentMethod method) => method;
//     private static PaymentStatus GetPaymentStatus(PaymentStatus status) => status;

//     public static void Seed(this ModelBuilder modelBuilder)
//     {
//         SeedUsers(modelBuilder);
//         SeedCategories(modelBuilder);
//         SeedProducts(modelBuilder);
//         SeedOrders(modelBuilder);
//         SeedOrderItems(modelBuilder);
//         SeedPayments(modelBuilder);
//     }

//     private static void SeedUsers(ModelBuilder modelBuilder)
//     {
//         var users = new List<User>
//         {
//             new User
//             {
//                 Id = "a1b2c3d4-e5f6-4a70-8901-000000000001",  
//                 UserName = "john.doe@email.com",
//                 NormalizedUserName = "JOHN.DOE@EMAIL.COM",
//                 Email = "john.doe@email.com",
//                 NormalizedEmail = "JOHN.DOE@EMAIL.COM",
//                 EmailConfirmed = true,
//                 PasswordHash = "AQAAAAIAAYagAAAAEP0Z0XG0bQn1HkxNVwOMj0vFhJYz0iM6w7ZWxHsiZ0XdHj8z0YwVPqP0Km0Pz0Vz0Q==",
//                 SecurityStamp = "7QJZ0XG0BQN1HKXNVWOMJ0VFHJYZ0IM6W7ZWXHSIZ0XDHJ8Z0YWV",
//                 ConcurrencyStamp = "a0b1c2d3-e4f5-6789-abcd-ef0123456789",
//                 PhoneNumber = "123-456-7890",
//                 PhoneNumberConfirmed = true,
//                 TwoFactorEnabled = false,
//                 LockoutEnabled = true,
//                 AccessFailedCount = 0,
//                 Address = "123 Main Street, New York, NY 10001",
//                 FullName = "John Doe"
//             },
//             new User
//             {
//                 Id = "b2c3d4e5-f6a7-4b80-9012-000000000002",  
//                 UserName = "jane.smith@email.com",
//                 NormalizedUserName = "JANE.SMITH@EMAIL.COM",
//                 Email = "jane.smith@email.com",
//                 NormalizedEmail = "JANE.SMITH@EMAIL.COM",
//                 EmailConfirmed = true,
//                 PasswordHash = "AQAAAAIAAYagAAAAEP0Z0XG0bQn1HkxNVwOMj0vFhJYz0iM6w7ZWxHsiZ0XdHj8z0YwVPqP0Km0Pz0Vz0Q==",
//                 SecurityStamp = "8RKZ1YH1CRP2ILYOWXPNK1VGKIZA1JN7X8AXYITJ1YEIK8A1ZXW",
//                 ConcurrencyStamp = "b1c2d3e4-f5a6-7890-bcde-f12345678901",
//                 PhoneNumber = "098-765-4321",
//                 PhoneNumberConfirmed = true,
//                 TwoFactorEnabled = false,
//                 LockoutEnabled = true,
//                 AccessFailedCount = 0,
//                 Address = "456 Oak Avenue, Los Angeles, CA 90001",
//                 FullName = "Jane Smith"
//             }
//         };

//         modelBuilder.Entity<User>().HasData(users);
//     }

//     private static void SeedCategories(ModelBuilder modelBuilder)
//     {
//         var categories = new[]
//         {
//             new Category
//             {
//                 CategoryId = Guid.Parse("c1a2b3c4-d5e6-4f70-8901-234567890abc"),
//                 Name = "Electronics",
//                 CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
//             },
//             new Category
//             {
//                 CategoryId = Guid.Parse("d2b3c4d5-e6f7-4a80-9012-345678901bcd"),
//                 Name = "Clothing",
//                 CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
//             },
//             new Category
//             {
//                 CategoryId = Guid.Parse("e3c4d5e6-f7a8-4b90-0123-456789012cde"),
//                 Name = "Books",
//                 CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
//             }
//         };

//         modelBuilder.Entity<Category>().HasData(categories);
//     }

//     private static void SeedProducts(ModelBuilder modelBuilder)
//     {
//         var products = new[]
//         {
//             new Product
//             {
//                 ProductId = Guid.Parse("a1b2c3d4-e5f6-4a70-8901-1234567890ab"),
//                 Name = "Smartphone X1",
//                 Price = 699.99m,
//                 StockQuantity = 50,
//                 Description = "Latest smartphone with advanced features and high-resolution camera",
//                 CategoryId = Guid.Parse("c1a2b3c4-d5e6-4f70-8901-234567890abc")
//             },
//             new Product
//             {
//                 ProductId = Guid.Parse("b2c3d4e5-f6a7-4b80-9012-2345678901bc"),
//                 Name = "Laptop Pro 15",
//                 Price = 1299.99m,
//                 StockQuantity = 25,
//                 Description = "Powerful laptop for professionals with 15-inch display and long battery life",
//                 CategoryId = Guid.Parse("c1a2b3c4-d5e6-4f70-8901-234567890abc")
//             },
//             new Product
//             {
//                 ProductId = Guid.Parse("c3d4e5f6-a7b8-4c90-0123-3456789012cd"),
//                 Name = "Cotton T-Shirt",
//                 Price = 24.99m,
//                 StockQuantity = 200,
//                 Description = "Comfortable 100% cotton t-shirt available in multiple colors",
//                 CategoryId = Guid.Parse("d2b3c4d5-e6f7-4a80-9012-345678901bcd")
//             },
//             new Product
//             {
//                 ProductId = Guid.Parse("d4e5f6a7-b8c9-4d01-1234-4567890123de"),
//                 Name = "Designer Jeans",
//                 Price = 89.99m,
//                 StockQuantity = 75,
//                 Description = "Premium quality designer jeans with modern fit and style",
//                 CategoryId = Guid.Parse("d2b3c4d5-e6f7-4a80-9012-345678901bcd")
//             },
//             new Product
//             {
//                 ProductId = Guid.Parse("e5f6a7b8-c9d0-4e12-2345-5678901234ef"),
//                 Name = "C# Programming Guide",
//                 Price = 49.99m,
//                 StockQuantity = 100,
//                 Description = "Comprehensive guide to C# programming from basics to advanced topics",
//                 CategoryId = Guid.Parse("e3c4d5e6-f7a8-4b90-0123-456789012cde")
//             },
//             new Product
//             {
//                 ProductId = Guid.Parse("f6a7b8c9-d0e1-4f23-3456-6789012345fa"),
//                 Name = "Web Development Mastery",
//                 Price = 39.99m,
//                 StockQuantity = 80,
//                 Description = "Complete guide to modern web development with ASP.NET Core",
//                 CategoryId = Guid.Parse("e3c4d5e6-f7a8-4b90-0123-456789012cde")
//             }
//         };

//         modelBuilder.Entity<Product>().HasData(products);
//     }

//     private static void SeedOrders(ModelBuilder modelBuilder)
//     {
//         var orders = new[]
//         {
//             new Order
//             {
//                 OrderId = Guid.Parse("1a2b3c4d-5e6f-4a70-8901-abcdef123456"),
//                 UserId = "a1b2c3d4-e5f6-4a70-8901-00000001",
//                 TotalPrice = 724.98m,
//                 OrderDate = new DateTime(2025, 1, 15, 10, 30, 0, DateTimeKind.Utc),
//                 Status = OrderStatus.Delivered
//             },
//             new Order
//             {
//                 OrderId = Guid.Parse("2b3c4d5e-6f7a-4b80-9012-bcdef1234567"),
//                 UserId = "a1b2c3d4-e5f6-4a70-8901-00000001",
//                 TotalPrice = 1389.98m,
//                 OrderDate = new DateTime(2025, 3, 20, 14, 45, 0, DateTimeKind.Utc),
//                 Status = OrderStatus.Confirmed
//             },
//             new Order
//             {
//                 OrderId = Guid.Parse("3c4d5e6f-7a8b-4c90-0123-cdef12345678"),
//                 UserId = "b2c3d4e5-f6a7-4b80-9012-00000002",
//                 TotalPrice = 114.98m,
//                 OrderDate = new DateTime(2025, 2, 10, 9, 15, 0, DateTimeKind.Utc),
//                 Status = OrderStatus.Delivered
//             },
//             new Order
//             {
//                 OrderId = Guid.Parse("4d5e6f7a-8b9c-4d01-1234-def123456789"),
//                 UserId = "b2c3d4e5-f6a7-4b80-9012-00000002",
//                 TotalPrice = 49.99m,
//                 OrderDate = new DateTime(2025, 5, 5, 16, 0, 0, DateTimeKind.Utc),
//                 Status = OrderStatus.Pending
//             }
//         };

//         modelBuilder.Entity<Order>().HasData(orders);
//     }

//     private static void SeedOrderItems(ModelBuilder modelBuilder)
//     {
//         var orderItems = new[]
//         {
//             new OrderItem
//             {
//                 OrderItemId = Guid.Parse("oi01-c3d4-e5f6-4a70-8901-0000000001"),
//                 OrderId = Guid.Parse("1a2b3c4d-5e6f-4a70-8901-abcdef123456"),
//                 ProductId = Guid.Parse("a1b2c3d4-e5f6-4a70-8901-1234567890ab"),
//                 Quantity = 1,
//                 PriceAtPurchase = 699.99m
//             },
//             new OrderItem
//             {
//                 OrderItemId = Guid.Parse("oi02-d4e5-f6a7-4b80-8901-0000000002"),
//                 OrderId = Guid.Parse("1a2b3c4d-5e6f-4a70-8901-abcdef123456"),
//                 ProductId = Guid.Parse("c3d4e5f6-a7b8-4c90-0123-3456789012cd"),
//                 Quantity = 1,
//                 PriceAtPurchase = 24.99m
//             },
//             new OrderItem
//             {
//                 OrderItemId = Guid.Parse("oi03-e5f6-a7b8-4c90-8901-0000000003"),
//                 OrderId = Guid.Parse("2b3c4d5e-6f7a-4b80-9012-bcdef1234567"),
//                 ProductId = Guid.Parse("b2c3d4e5-f6a7-4b80-9012-2345678901bc"),
//                 Quantity = 1,
//                 PriceAtPurchase = 1299.99m
//             },
//             new OrderItem
//             {
//                 OrderItemId = Guid.Parse("oi04-f6a7-b8c9-4d01-8901-0000000004"),
//                 OrderId = Guid.Parse("2b3c4d5e-6f7a-4b80-9012-bcdef1234567"),
//                 ProductId = Guid.Parse("d4e5f6a7-b8c9-4d01-1234-4567890123de"),
//                 Quantity = 1,
//                 PriceAtPurchase = 89.99m
//             },
//             new OrderItem
//             {
//                 OrderItemId = Guid.Parse("oi05-a7b8-c9d0-4e12-8901-0000000005"),
//                 OrderId = Guid.Parse("3c4d5e6f-7a8b-4c90-0123-cdef12345678"),
//                 ProductId = Guid.Parse("c3d4e5f6-a7b8-4c90-0123-3456789012cd"),
//                 Quantity = 2,
//                 PriceAtPurchase = 24.99m
//             },
//             new OrderItem
//             {
//                 OrderItemId = Guid.Parse("oi06-b8c9-d0e1-4f23-8901-0000000006"),
//                 OrderId = Guid.Parse("3c4d5e6f-7a8b-4c90-0123-cdef12345678"),
//                 ProductId = Guid.Parse("e5f6a7b8-c9d0-4e12-2345-5678901234ef"),
//                 Quantity = 1,
//                 PriceAtPurchase = 49.99m
//             },
//             new OrderItem
//             {
//                 OrderItemId = Guid.Parse("oi07-c9d0-e1f2-4a34-8901-0000000007"),
//                 OrderId = Guid.Parse("4d5e6f7a-8b9c-4d01-1234-def123456789"),
//                 ProductId = Guid.Parse("e5f6a7b8-c9d0-4e12-2345-5678901234ef"),
//                 Quantity = 1,
//                 PriceAtPurchase = 49.99m
//             }
//         };

//         modelBuilder.Entity<OrderItem>().HasData(orderItems);
//     }

//     private static void SeedPayments(ModelBuilder modelBuilder)
//     {
//         var payments = new[]
//         {
//             new Payment
//             {
//                 PaymentId = Guid.Parse("afcefe3a-5b81-4e95-b06a-111111111111"),
//                 OrderId = Guid.Parse("1a2b3c4d-5e6f-4a70-8901-abcdef123456"),
//                 PayMethod = PaymentMethod.CreditCard, // CreditCard
//                 PayDate = new DateTime(2025, 1, 15, 10, 35, 0, DateTimeKind.Utc),
//                 PayStatus = PaymentStatus.Success // Success
//             },
//             new Payment
//             {
//                 PaymentId = Guid.Parse("bfcefe3a-5b81-4e95-b06a-222222222222"),
//                 OrderId = Guid.Parse("2b3c4d5e-6f7a-4b80-9012-bcdef1234567"),
//                 PayMethod = PaymentMethod.CreditCard, // Wallet
//                 PayDate = new DateTime(2025, 3, 20, 14, 50, 0, DateTimeKind.Utc),
//                 PayStatus = PaymentStatus.Success // Success
//             },
//             new Payment
//             {
//                 PaymentId = Guid.Parse("cfcefe3a-5b81-4e95-b06a-333333333333"),
//                 OrderId = Guid.Parse("3c4d5e6f-7a8b-4c90-0123-cdef12345678"),
//                 PayMethod = PaymentMethod.CreditCard, // CreditCard
//                 PayDate = new DateTime(2025, 2, 10, 9, 20, 0, DateTimeKind.Utc),
//                 PayStatus = PaymentStatus.Success // Success
//             },
//             new Payment
//             {
//                 PaymentId = Guid.Parse("dfcefe3a-5b81-4e95-b06a-444444444444"),
//                 OrderId = Guid.Parse("4d5e6f7a-8b9c-4d01-1234-def123456789"),
//                 PayMethod = PaymentMethod.Wallet, // Wallet
//                 PayDate = new DateTime(2025, 5, 5, 16, 5, 0, DateTimeKind.Utc),
//                 PayStatus = PaymentStatus.Pending // Pending
//             }
//         };

//         modelBuilder.Entity<Payment>().HasData(payments);
//     }
// }









using Core.Entities;
using Core.Enum.OrderStatus;
using Core.Enum.PaymentMethod;
using Core.Enum.PaymentStatus;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public static class SeedData
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        SeedUsers(modelBuilder);
        SeedCategories(modelBuilder);
        SeedProducts(modelBuilder);
        SeedOrders(modelBuilder);
        SeedOrderItems(modelBuilder);
        SeedPayments(modelBuilder);
    }

    private static void SeedUsers(ModelBuilder modelBuilder)
    {
        var users = new List<User>
        {
            new User
            {
                Id = "a1b2c3d4-e5f6-4a70-8901-000000000001",  
                UserName = "john.doe@email.com",
                NormalizedUserName = "JOHN.DOE@EMAIL.COM",
                Email = "john.doe@email.com",
                NormalizedEmail = "JOHN.DOE@EMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEP0Z0XG0bQn1HkxNVwOMj0vFhJYz0iM6w7ZWxHsiZ0XdHj8z0YwVPqP0Km0Pz0Vz0Q==",
                SecurityStamp = "7QJZ0XG0BQN1HKXNVWOMJ0VFHJYZ0IM6W7ZWXHSIZ0XDHJ8Z0YWV",
                ConcurrencyStamp = "a0b1c2d3-e4f5-6789-abcd-ef0123456789",
                PhoneNumber = "123-456-7890",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                Address = "123 Main Street, New York, NY 10001",
                FullName = "John Doe"
            },
            new User
            {
                Id = "b2c3d4e5-f6a7-4b80-9012-000000000002",  
                UserName = "jane.smith@email.com",
                NormalizedUserName = "JANE.SMITH@EMAIL.COM",
                Email = "jane.smith@email.com",
                NormalizedEmail = "JANE.SMITH@EMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEP0Z0XG0bQn1HkxNVwOMj0vFhJYz0iM6w7ZWxHsiZ0XdHj8z0YwVPqP0Km0Pz0Vz0Q==",
                SecurityStamp = "8RKZ1YH1CRP2ILYOWXPNK1VGKIZA1JN7X8AXYITJ1YEIK8A1ZXW",
                ConcurrencyStamp = "b1c2d3e4-f5a6-7890-bcde-f12345678901",
                PhoneNumber = "098-765-4321",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                Address = "456 Oak Avenue, Los Angeles, CA 90001",
                FullName = "Jane Smith"
            }
        };

        modelBuilder.Entity<User>().HasData(users);
    }

    private static void SeedCategories(ModelBuilder modelBuilder)
    {
        var categories = new[]
        {
            new Category
            {
                CategoryId = Guid.Parse("c1a2b3c4-d5e6-4f70-8901-234567890abc"),
                Name = "Electronics",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                CategoryId = Guid.Parse("d2b3c4d5-e6f7-4a80-9012-345678901bcd"),
                Name = "Clothing",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Category
            {
                CategoryId = Guid.Parse("e3c4d5e6-f7a8-4b90-0123-456789012cde"),
                Name = "Books",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        };

        modelBuilder.Entity<Category>().HasData(categories);
    }

    private static void SeedProducts(ModelBuilder modelBuilder)
    {
        var products = new[]
        {
            new Product
            {
                ProductId = Guid.Parse("a1b2c3d4-e5f6-4a70-8901-1234567890ab"),
                Name = "Smartphone X1",
                Price = 699.99m,
                StockQuantity = 50,
                Description = "Latest smartphone with advanced features and high-resolution camera",
                CategoryId = Guid.Parse("c1a2b3c4-d5e6-4f70-8901-234567890abc")
            },
            new Product
            {
                ProductId = Guid.Parse("b2c3d4e5-f6a7-4b80-9012-2345678901bc"),
                Name = "Laptop Pro 15",
                Price = 1299.99m,
                StockQuantity = 25,
                Description = "Powerful laptop for professionals with 15-inch display and long battery life",
                CategoryId = Guid.Parse("c1a2b3c4-d5e6-4f70-8901-234567890abc")
            },
            new Product
            {
                ProductId = Guid.Parse("c3d4e5f6-a7b8-4c90-0123-3456789012cd"),
                Name = "Cotton T-Shirt",
                Price = 24.99m,
                StockQuantity = 200,
                Description = "Comfortable 100% cotton t-shirt available in multiple colors",
                CategoryId = Guid.Parse("d2b3c4d5-e6f7-4a80-9012-345678901bcd")
            },
            new Product
            {
                ProductId = Guid.Parse("d4e5f6a7-b8c9-4d01-1234-4567890123de"),
                Name = "Designer Jeans",
                Price = 89.99m,
                StockQuantity = 75,
                Description = "Premium quality designer jeans with modern fit and style",
                CategoryId = Guid.Parse("d2b3c4d5-e6f7-4a80-9012-345678901bcd")
            },
            new Product
            {
                ProductId = Guid.Parse("e5f6a7b8-c9d0-4e12-2345-5678901234ef"),
                Name = "C# Programming Guide",
                Price = 49.99m,
                StockQuantity = 100,
                Description = "Comprehensive guide to C# programming from basics to advanced topics",
                CategoryId = Guid.Parse("e3c4d5e6-f7a8-4b90-0123-456789012cde")
            },
            new Product
            {
                ProductId = Guid.Parse("f6a7b8c9-d0e1-4f23-3456-6789012345fa"),
                Name = "Web Development Mastery",
                Price = 39.99m,
                StockQuantity = 80,
                Description = "Complete guide to modern web development with ASP.NET Core",
                CategoryId = Guid.Parse("e3c4d5e6-f7a8-4b90-0123-456789012cde")
            }
        };

        modelBuilder.Entity<Product>().HasData(products);
    }

    private static void SeedOrders(ModelBuilder modelBuilder)
    {
        var orders = new[]
        {
            new Order
            {
                OrderId = Guid.Parse("1a2b3c4d-5e6f-4a70-8901-abcdef123456"),
                UserId = "a1b2c3d4-e5f6-4a70-8901-000000000001",  // ✅ Fixed: matches User 1
                TotalPrice = 724.98m,
                OrderDate = new DateTime(2025, 1, 15, 10, 30, 0, DateTimeKind.Utc),
                Status = OrderStatus.Delivered
            },
            new Order
            {
                OrderId = Guid.Parse("2b3c4d5e-6f7a-4b80-9012-bcdef1234567"),
                UserId = "a1b2c3d4-e5f6-4a70-8901-000000000001",  // ✅ Fixed: matches User 1
                TotalPrice = 1389.98m,
                OrderDate = new DateTime(2025, 3, 20, 14, 45, 0, DateTimeKind.Utc),
                Status = OrderStatus.Confirmed
            },
            new Order
            {
                OrderId = Guid.Parse("3c4d5e6f-7a8b-4c90-0123-cdef12345678"),
                UserId = "b2c3d4e5-f6a7-4b80-9012-000000000002",  // ✅ Fixed: matches User 2
                TotalPrice = 114.98m,
                OrderDate = new DateTime(2025, 2, 10, 9, 15, 0, DateTimeKind.Utc),
                Status = OrderStatus.Delivered
            },
            new Order
            {
                OrderId = Guid.Parse("4d5e6f7a-8b9c-4d01-1234-def123456789"),
                UserId = "b2c3d4e5-f6a7-4b80-9012-000000000002",  // ✅ Fixed: matches User 2
                TotalPrice = 49.99m,
                OrderDate = new DateTime(2025, 5, 5, 16, 0, 0, DateTimeKind.Utc),
                Status = OrderStatus.Pending
            }
        };

        modelBuilder.Entity<Order>().HasData(orders);
    }

    private static void SeedOrderItems(ModelBuilder modelBuilder)
    {
        var orderItems = new[]
        {
            new OrderItem
            {
                OrderItemId = Guid.Parse("0a1b2c3d-0001-4a70-8901-000000000001"),  // ✅ Fixed: valid GUID
                OrderId = Guid.Parse("1a2b3c4d-5e6f-4a70-8901-abcdef123456"),
                ProductId = Guid.Parse("a1b2c3d4-e5f6-4a70-8901-1234567890ab"),
                Quantity = 1,
                PriceAtPurchase = 699.99m
            },
            new OrderItem
            {
                OrderItemId = Guid.Parse("0a1b2c3d-0002-4a70-8901-000000000002"),  // ✅ Fixed: valid GUID
                OrderId = Guid.Parse("1a2b3c4d-5e6f-4a70-8901-abcdef123456"),
                ProductId = Guid.Parse("c3d4e5f6-a7b8-4c90-0123-3456789012cd"),
                Quantity = 1,
                PriceAtPurchase = 24.99m
            },
            new OrderItem
            {
                OrderItemId = Guid.Parse("0a1b2c3d-0003-4a70-8901-000000000003"),  // ✅ Fixed: valid GUID
                OrderId = Guid.Parse("2b3c4d5e-6f7a-4b80-9012-bcdef1234567"),
                ProductId = Guid.Parse("b2c3d4e5-f6a7-4b80-9012-2345678901bc"),
                Quantity = 1,
                PriceAtPurchase = 1299.99m
            },
            new OrderItem
            {
                OrderItemId = Guid.Parse("0a1b2c3d-0004-4a70-8901-000000000004"),  // ✅ Fixed: valid GUID
                OrderId = Guid.Parse("2b3c4d5e-6f7a-4b80-9012-bcdef1234567"),
                ProductId = Guid.Parse("d4e5f6a7-b8c9-4d01-1234-4567890123de"),
                Quantity = 1,
                PriceAtPurchase = 89.99m
            },
            new OrderItem
            {
                OrderItemId = Guid.Parse("0a1b2c3d-0005-4a70-8901-000000000005"),  // ✅ Fixed: valid GUID
                OrderId = Guid.Parse("3c4d5e6f-7a8b-4c90-0123-cdef12345678"),
                ProductId = Guid.Parse("c3d4e5f6-a7b8-4c90-0123-3456789012cd"),
                Quantity = 2,
                PriceAtPurchase = 24.99m
            },
            new OrderItem
            {
                OrderItemId = Guid.Parse("0a1b2c3d-0006-4a70-8901-000000000006"),  // ✅ Fixed: valid GUID
                OrderId = Guid.Parse("3c4d5e6f-7a8b-4c90-0123-cdef12345678"),
                ProductId = Guid.Parse("e5f6a7b8-c9d0-4e12-2345-5678901234ef"),
                Quantity = 1,
                PriceAtPurchase = 49.99m
            },
            new OrderItem
            {
                OrderItemId = Guid.Parse("0a1b2c3d-0007-4a70-8901-000000000007"),  // ✅ Fixed: valid GUID
                OrderId = Guid.Parse("4d5e6f7a-8b9c-4d01-1234-def123456789"),
                ProductId = Guid.Parse("e5f6a7b8-c9d0-4e12-2345-5678901234ef"),
                Quantity = 1,
                PriceAtPurchase = 49.99m
            }
        };

        modelBuilder.Entity<OrderItem>().HasData(orderItems);
    }

    private static void SeedPayments(ModelBuilder modelBuilder)
    {
        var payments = new[]
        {
            new Payment
            {
                PaymentId = Guid.Parse("afcefe3a-5b81-4e95-b06a-111111111111"),
                OrderId = Guid.Parse("1a2b3c4d-5e6f-4a70-8901-abcdef123456"),
                PayMethod = PaymentMethod.CreditCard,
                PayDate = new DateTime(2025, 1, 15, 10, 35, 0, DateTimeKind.Utc),
                PayStatus = PaymentStatus.Success
            },
            new Payment
            {
                PaymentId = Guid.Parse("bfcefe3a-5b81-4e95-b06a-222222222222"),
                OrderId = Guid.Parse("2b3c4d5e-6f7a-4b80-9012-bcdef1234567"),
                PayMethod = PaymentMethod.Wallet,
                PayDate = new DateTime(2025, 3, 20, 14, 50, 0, DateTimeKind.Utc),
                PayStatus = PaymentStatus.Success
            },
            new Payment
            {
                PaymentId = Guid.Parse("cfcefe3a-5b81-4e95-b06a-333333333333"),
                OrderId = Guid.Parse("3c4d5e6f-7a8b-4c90-0123-cdef12345678"),
                PayMethod = PaymentMethod.CreditCard,
                PayDate = new DateTime(2025, 2, 10, 9, 20, 0, DateTimeKind.Utc),
                PayStatus = PaymentStatus.Success
            },
            new Payment
            {
                PaymentId = Guid.Parse("dfcefe3a-5b81-4e95-b06a-444444444444"),
                OrderId = Guid.Parse("4d5e6f7a-8b9c-4d01-1234-def123456789"),
                PayMethod = PaymentMethod.Wallet,
                PayDate = new DateTime(2025, 5, 5, 16, 5, 0, DateTimeKind.Utc),
                PayStatus = PaymentStatus.Pending
            }
        };

        modelBuilder.Entity<Payment>().HasData(payments);
    }
}