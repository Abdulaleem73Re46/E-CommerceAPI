using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerce.Repository.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "AAAAAAAA-BBBB-CCCC-DDDD-000000000001");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "AAAAAAAA-BBBB-CCCC-DDDD-000000000002");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("a1888458-6436-4c23-93ef-21ec4cd96972"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("fa259fd7-771e-42f5-9774-0b0d96cb0ecd"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("fcd7872b-00db-47ed-b451-c27dbd9d92ec"));

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "a1b2c3d4-e5f6-4a70-8901-000000000001", 0, "123 Main Street, New York, NY 10001", "a0b1c2d3-e4f5-6789-abcd-ef0123456789", "john.doe@email.com", true, "John Doe", true, null, "JOHN.DOE@EMAIL.COM", "JOHN.DOE@EMAIL.COM", "AQAAAAIAAYagAAAAEP0Z0XG0bQn1HkxNVwOMj0vFhJYz0iM6w7ZWxHsiZ0XdHj8z0YwVPqP0Km0Pz0Vz0Q==", "123-456-7890", true, "7QJZ0XG0BQN1HKXNVWOMJ0VFHJYZ0IM6W7ZWXHSIZ0XDHJ8Z0YWV", false, "john.doe@email.com" },
                    { "b2c3d4e5-f6a7-4b80-9012-000000000002", 0, "456 Oak Avenue, Los Angeles, CA 90001", "b1c2d3e4-f5a6-7890-bcde-f12345678901", "jane.smith@email.com", true, "Jane Smith", true, null, "JANE.SMITH@EMAIL.COM", "JANE.SMITH@EMAIL.COM", "AQAAAAIAAYagAAAAEP0Z0XG0bQn1HkxNVwOMj0vFhJYz0iM6w7ZWxHsiZ0XdHj8z0YwVPqP0Km0Pz0Vz0Q==", "098-765-4321", true, "8RKZ1YH1CRP2ILYOWXPNK1VGKIZA1JN7X8AXYITJ1YEIK8A1ZXW", false, "jane.smith@email.com" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("c1a2b3c4-d5e6-4f70-8901-234567890abc"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Electronics" },
                    { new Guid("d2b3c4d5-e6f7-4a80-9012-345678901bcd"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Clothing" },
                    { new Guid("e3c4d5e6-f7a8-4b90-0123-456789012cde"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Books" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "OrderDate", "Status", "TotalPrice", "UserId" },
                values: new object[,]
                {
                    { new Guid("1a2b3c4d-5e6f-4a70-8901-abcdef123456"), new DateTime(2025, 1, 15, 10, 30, 0, 0, DateTimeKind.Utc), 4, 724.98m, "a1b2c3d4-e5f6-4a70-8901-000000000001" },
                    { new Guid("2b3c4d5e-6f7a-4b80-9012-bcdef1234567"), new DateTime(2025, 3, 20, 14, 45, 0, 0, DateTimeKind.Utc), 1, 1389.98m, "a1b2c3d4-e5f6-4a70-8901-000000000001" },
                    { new Guid("3c4d5e6f-7a8b-4c90-0123-cdef12345678"), new DateTime(2025, 2, 10, 9, 15, 0, 0, DateTimeKind.Utc), 4, 114.98m, "b2c3d4e5-f6a7-4b80-9012-000000000002" },
                    { new Guid("4d5e6f7a-8b9c-4d01-1234-def123456789"), new DateTime(2025, 5, 5, 16, 0, 0, 0, DateTimeKind.Utc), 0, 49.99m, "b2c3d4e5-f6a7-4b80-9012-000000000002" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "Description", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-4a70-8901-1234567890ab"), new Guid("c1a2b3c4-d5e6-4f70-8901-234567890abc"), "Latest smartphone with advanced features and high-resolution camera", "Smartphone X1", 699.99m, 50 },
                    { new Guid("b2c3d4e5-f6a7-4b80-9012-2345678901bc"), new Guid("c1a2b3c4-d5e6-4f70-8901-234567890abc"), "Powerful laptop for professionals with 15-inch display and long battery life", "Laptop Pro 15", 1299.99m, 25 },
                    { new Guid("c3d4e5f6-a7b8-4c90-0123-3456789012cd"), new Guid("d2b3c4d5-e6f7-4a80-9012-345678901bcd"), "Comfortable 100% cotton t-shirt available in multiple colors", "Cotton T-Shirt", 24.99m, 200 },
                    { new Guid("d4e5f6a7-b8c9-4d01-1234-4567890123de"), new Guid("d2b3c4d5-e6f7-4a80-9012-345678901bcd"), "Premium quality designer jeans with modern fit and style", "Designer Jeans", 89.99m, 75 },
                    { new Guid("e5f6a7b8-c9d0-4e12-2345-5678901234ef"), new Guid("e3c4d5e6-f7a8-4b90-0123-456789012cde"), "Comprehensive guide to C# programming from basics to advanced topics", "C# Programming Guide", 49.99m, 100 },
                    { new Guid("f6a7b8c9-d0e1-4f23-3456-6789012345fa"), new Guid("e3c4d5e6-f7a8-4b90-0123-456789012cde"), "Complete guide to modern web development with ASP.NET Core", "Web Development Mastery", 39.99m, 80 }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "OrderItemId", "OrderId", "PriceAtPurchase", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { new Guid("0a1b2c3d-0001-4a70-8901-000000000001"), new Guid("1a2b3c4d-5e6f-4a70-8901-abcdef123456"), 699.99m, new Guid("a1b2c3d4-e5f6-4a70-8901-1234567890ab"), 1 },
                    { new Guid("0a1b2c3d-0002-4a70-8901-000000000002"), new Guid("1a2b3c4d-5e6f-4a70-8901-abcdef123456"), 24.99m, new Guid("c3d4e5f6-a7b8-4c90-0123-3456789012cd"), 1 },
                    { new Guid("0a1b2c3d-0003-4a70-8901-000000000003"), new Guid("2b3c4d5e-6f7a-4b80-9012-bcdef1234567"), 1299.99m, new Guid("b2c3d4e5-f6a7-4b80-9012-2345678901bc"), 1 },
                    { new Guid("0a1b2c3d-0004-4a70-8901-000000000004"), new Guid("2b3c4d5e-6f7a-4b80-9012-bcdef1234567"), 89.99m, new Guid("d4e5f6a7-b8c9-4d01-1234-4567890123de"), 1 },
                    { new Guid("0a1b2c3d-0005-4a70-8901-000000000005"), new Guid("3c4d5e6f-7a8b-4c90-0123-cdef12345678"), 24.99m, new Guid("c3d4e5f6-a7b8-4c90-0123-3456789012cd"), 2 },
                    { new Guid("0a1b2c3d-0006-4a70-8901-000000000006"), new Guid("3c4d5e6f-7a8b-4c90-0123-cdef12345678"), 49.99m, new Guid("e5f6a7b8-c9d0-4e12-2345-5678901234ef"), 1 },
                    { new Guid("0a1b2c3d-0007-4a70-8901-000000000007"), new Guid("4d5e6f7a-8b9c-4d01-1234-def123456789"), 49.99m, new Guid("e5f6a7b8-c9d0-4e12-2345-5678901234ef"), 1 }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentId", "OrderId", "PayDate", "PayMethod", "PayStatus" },
                values: new object[,]
                {
                    { new Guid("afcefe3a-5b81-4e95-b06a-111111111111"), new Guid("1a2b3c4d-5e6f-4a70-8901-abcdef123456"), new DateTime(2025, 1, 15, 10, 35, 0, 0, DateTimeKind.Utc), 0, 1 },
                    { new Guid("bfcefe3a-5b81-4e95-b06a-222222222222"), new Guid("2b3c4d5e-6f7a-4b80-9012-bcdef1234567"), new DateTime(2025, 3, 20, 14, 50, 0, 0, DateTimeKind.Utc), 1, 1 },
                    { new Guid("cfcefe3a-5b81-4e95-b06a-333333333333"), new Guid("3c4d5e6f-7a8b-4c90-0123-cdef12345678"), new DateTime(2025, 2, 10, 9, 20, 0, 0, DateTimeKind.Utc), 0, 1 },
                    { new Guid("dfcefe3a-5b81-4e95-b06a-444444444444"), new Guid("4d5e6f7a-8b9c-4d01-1234-def123456789"), new DateTime(2025, 5, 5, 16, 5, 0, 0, DateTimeKind.Utc), 1, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: new Guid("0a1b2c3d-0001-4a70-8901-000000000001"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: new Guid("0a1b2c3d-0002-4a70-8901-000000000002"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: new Guid("0a1b2c3d-0003-4a70-8901-000000000003"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: new Guid("0a1b2c3d-0004-4a70-8901-000000000004"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: new Guid("0a1b2c3d-0005-4a70-8901-000000000005"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: new Guid("0a1b2c3d-0006-4a70-8901-000000000006"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: new Guid("0a1b2c3d-0007-4a70-8901-000000000007"));

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: new Guid("afcefe3a-5b81-4e95-b06a-111111111111"));

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: new Guid("bfcefe3a-5b81-4e95-b06a-222222222222"));

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: new Guid("cfcefe3a-5b81-4e95-b06a-333333333333"));

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: new Guid("dfcefe3a-5b81-4e95-b06a-444444444444"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("f6a7b8c9-d0e1-4f23-3456-6789012345fa"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("1a2b3c4d-5e6f-4a70-8901-abcdef123456"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("2b3c4d5e-6f7a-4b80-9012-bcdef1234567"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("3c4d5e6f-7a8b-4c90-0123-cdef12345678"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("4d5e6f7a-8b9c-4d01-1234-def123456789"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("a1b2c3d4-e5f6-4a70-8901-1234567890ab"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("b2c3d4e5-f6a7-4b80-9012-2345678901bc"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("c3d4e5f6-a7b8-4c90-0123-3456789012cd"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("d4e5f6a7-b8c9-4d01-1234-4567890123de"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("e5f6a7b8-c9d0-4e12-2345-5678901234ef"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-e5f6-4a70-8901-000000000001");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b2c3d4e5-f6a7-4b80-9012-000000000002");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("c1a2b3c4-d5e6-4f70-8901-234567890abc"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("d2b3c4d5-e6f7-4a80-9012-345678901bcd"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("e3c4d5e6-f7a8-4b90-0123-456789012cde"));

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "AAAAAAAA-BBBB-CCCC-DDDD-000000000001", 0, "123 Main Street, New York, NY 10001", "11111111-1111-1111-1111-111111111111", "john.doe@email.com", true, "John Doe", true, null, "JOHN.DOE@EMAIL.COM", "JOHN.DOE@EMAIL.COM", "AQAAAAIAAYagAAAAEP0Z0XG0bQn1HkxNVwOMj0vFhJYz0iM6w7ZWxHsiZ0XdHj8z0YwVPqP0Km0Pz0Vz0Q==", "123-456-7890", true, "SECURITYSTAMP000000000000000000001", false, "john.doe@email.com" },
                    { "AAAAAAAA-BBBB-CCCC-DDDD-000000000002", 0, "456 Oak Avenue, Los Angeles, CA 90001", "22222222-2222-2222-2222-222222222222", "jane.smith@email.com", true, "Jane Smith", true, null, "JANE.SMITH@EMAIL.COM", "JANE.SMITH@EMAIL.COM", "AQAAAAIAAYagAAAAEP0Z0XG0bQn1HkxNVwOMj0vFhJYz0iM6w7ZWxHsiZ0XdHj8z0YwVPqP0Km0Pz0Vz0Q==", "098-765-4321", true, "SECURITYSTAMP000000000000000000002", false, "jane.smith@email.com" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("a1888458-6436-4c23-93ef-21ec4cd96972"), new DateTime(2026, 5, 12, 18, 3, 40, 935, DateTimeKind.Utc).AddTicks(6357), "Books" },
                    { new Guid("fa259fd7-771e-42f5-9774-0b0d96cb0ecd"), new DateTime(2026, 5, 12, 18, 3, 40, 935, DateTimeKind.Utc).AddTicks(6355), "Clothing" },
                    { new Guid("fcd7872b-00db-47ed-b451-c27dbd9d92ec"), new DateTime(2026, 5, 12, 18, 3, 40, 935, DateTimeKind.Utc).AddTicks(6082), "Electronics" }
                });
        }
    }
}
