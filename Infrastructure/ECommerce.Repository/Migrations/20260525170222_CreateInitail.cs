using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerce.Repository.Migrations
{
    /// <inheritdoc />
    public partial class CreateInitail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    FullName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PayMethod = table.Column<string>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    PayDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PayStatus = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    CartId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.CartId);
                    table.ForeignKey(
                        name: "FK_Carts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Token = table.Column<string>(type: "TEXT", nullable: false),
                    ExpiresOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    RevokedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => new { x.UserId, x.Id });
                    table.ForeignKey(
                        name: "FK_RefreshToken_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 700, nullable: false),
                    Version = table.Column<int>(type: "INTEGER", nullable: false),
                    CategoryId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    PaymentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "PaymentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CartId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 1),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "CartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderItemId = table.Column<Guid>(type: "TEXT", nullable: false),
                    OrderId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    PriceAtPurchase = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

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
                table: "Payments",
                columns: new[] { "PaymentId", "Amount", "PayDate", "PayMethod", "PayStatus" },
                values: new object[,]
                {
                    { new Guid("afcefe3a-5b81-4e95-b06a-111111111111"), 0m, new DateTime(2025, 1, 15, 10, 35, 0, 0, DateTimeKind.Utc), "CreditCard", "Success" },
                    { new Guid("bfcefe3a-5b81-4e95-b06a-222222222222"), 0m, new DateTime(2025, 3, 20, 14, 50, 0, 0, DateTimeKind.Utc), "Wallet", "Success" },
                    { new Guid("cfcefe3a-5b81-4e95-b06a-333333333333"), 0m, new DateTime(2025, 2, 10, 9, 20, 0, 0, DateTimeKind.Utc), "CreditCard", "Success" },
                    { new Guid("dfcefe3a-5b81-4e95-b06a-444444444444"), 0m, new DateTime(2025, 5, 5, 16, 5, 0, 0, DateTimeKind.Utc), "Wallet", "Pending" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "OrderDate", "PaymentId", "Status", "TotalPrice", "UserId" },
                values: new object[,]
                {
                    { new Guid("1a2b3c4d-5e6f-4a70-8901-abcdef123456"), new DateTime(2025, 1, 15, 10, 30, 0, 0, DateTimeKind.Utc), new Guid("afcefe3a-5b81-4e95-b06a-111111111111"), "Delivered", 724.98m, "a1b2c3d4-e5f6-4a70-8901-000000000001" },
                    { new Guid("2b3c4d5e-6f7a-4b80-9012-bcdef1234567"), new DateTime(2025, 3, 20, 14, 45, 0, 0, DateTimeKind.Utc), new Guid("bfcefe3a-5b81-4e95-b06a-222222222222"), "Confirmed", 1389.98m, "a1b2c3d4-e5f6-4a70-8901-000000000001" },
                    { new Guid("3c4d5e6f-7a8b-4c90-0123-cdef12345678"), new DateTime(2025, 2, 10, 9, 15, 0, 0, DateTimeKind.Utc), new Guid("cfcefe3a-5b81-4e95-b06a-333333333333"), "Delivered", 114.98m, "b2c3d4e5-f6a7-4b80-9012-000000000002" },
                    { new Guid("4d5e6f7a-8b9c-4d01-1234-def123456789"), new DateTime(2025, 5, 5, 16, 0, 0, 0, DateTimeKind.Utc), new Guid("dfcefe3a-5b81-4e95-b06a-444444444444"), "Pending", 49.99m, "b2c3d4e5-f6a7-4b80-9012-000000000002" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "Description", "Name", "Price", "StockQuantity", "Version" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-4a70-8901-1234567890ab"), new Guid("c1a2b3c4-d5e6-4f70-8901-234567890abc"), "Latest smartphone with advanced features and high-resolution camera", "Smartphone X1", 699.99m, 50, 0 },
                    { new Guid("b2c3d4e5-f6a7-4b80-9012-2345678901bc"), new Guid("c1a2b3c4-d5e6-4f70-8901-234567890abc"), "Powerful laptop for professionals with 15-inch display and long battery life", "Laptop Pro 15", 1299.99m, 25, 0 },
                    { new Guid("c3d4e5f6-a7b8-4c90-0123-3456789012cd"), new Guid("d2b3c4d5-e6f7-4a80-9012-345678901bcd"), "Comfortable 100% cotton t-shirt available in multiple colors", "Cotton T-Shirt", 24.99m, 200, 0 },
                    { new Guid("d4e5f6a7-b8c9-4d01-1234-4567890123de"), new Guid("d2b3c4d5-e6f7-4a80-9012-345678901bcd"), "Premium quality designer jeans with modern fit and style", "Designer Jeans", 89.99m, 75, 0 },
                    { new Guid("e5f6a7b8-c9d0-4e12-2345-5678901234ef"), new Guid("e3c4d5e6-f7a8-4b90-0123-456789012cde"), "Comprehensive guide to C# programming from basics to advanced topics", "C# Programming Guide", 49.99m, 100, 0 },
                    { new Guid("f6a7b8c9-d0e1-4f23-3456-6789012345fa"), new Guid("e3c4d5e6-f7a8-4b90-0123-456789012cde"), "Complete guide to modern web development with ASP.NET Core", "Web Development Mastery", 39.99m, 80, 0 }
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

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId_ProductId",
                table: "CartItems",
                columns: new[] { "CartId", "ProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentId",
                table: "Orders",
                column: "PaymentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
