using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vouchers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Code = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Factor = table.Column<float>(type: "real", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vouchers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ThumbnailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Images_ThumbnailId",
                        column: x => x.ThumbnailId,
                        principalTable: "Images",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Phone = table.Column<int>(type: "int", nullable: true),
                    AvatarId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: true),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Images_AvatarId",
                        column: x => x.AvatarId,
                        principalTable: "Images",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    BeforePrice = table.Column<float>(type: "real", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    TotalRating = table.Column<float>(type: "real", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    OrderTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BuyerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VoucherId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_OrderTypes_OrderTypeId",
                        column: x => x.OrderTypeId,
                        principalTable: "OrderTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Users_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Vouchers_VoucherId",
                        column: x => x.VoucherId,
                        principalTable: "Vouchers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Previews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Previews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Previews_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Previews_ProductDetails_ProductDetailId",
                        column: x => x.ProductDetailId,
                        principalTable: "ProductDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProductDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductOptions_ProductDetails_ProductDetailId",
                        column: x => x.ProductDetailId,
                        principalTable: "ProductDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    ProductDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_ProductDetails_ProductDetailId",
                        column: x => x.ProductDetailId,
                        principalTable: "ProductDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IsSelected = table.Column<bool>(type: "bit", nullable: false),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductOptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CartItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CartItems_ProductOptions_ProductOptionId",
                        column: x => x.ProductOptionId,
                        principalTable: "ProductOptions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0241b743-ca16-4c25-96da-a2cf73dc008e"), "Laptop" },
                    { new Guid("442f967f-1aa0-4aeb-8d35-c94fea58c98f"), "Smart Phone" },
                    { new Guid("54733eda-9e2f-4675-a25c-1e46a5d4a347"), "Tablet" },
                    { new Guid("b0326961-b2c9-4681-8e36-d892cc1ca7ed"), "Other" },
                    { new Guid("e75c26e7-ce66-4fd7-a55e-fee9eb20b3a4"), "Smart Watch" }
                });

            migrationBuilder.InsertData(
                table: "OrderTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("4f235200-957f-4051-83fa-93f0b7d5e2d3"), "confirmed" },
                    { new Guid("afb8d8b7-4035-42aa-a157-03f56d67c314"), "cancelled" },
                    { new Guid("f7e5cbee-e5de-4a7d-b688-5e56fdf96573"), "pending" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedOn", "ImageId", "Name", "ThumbnailId" },
                values: new object[] { new Guid("006b0864-fa5f-4657-8bd9-24ca15ed82ff"), null, new DateTime(2025, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Samsung A70", null });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("c26b7fcb-9e16-47aa-893e-3ef148de9714"), "Admin" },
                    { new Guid("f80eee5a-eefe-49c6-9a11-2e5b3804a71c"), "Customer" }
                });

            migrationBuilder.InsertData(
                table: "Vouchers",
                columns: new[] { "Id", "Code", "CreatedAt", "ExpiredAt", "Factor", "IsActive", "Name" },
                values: new object[] { new Guid("86a42389-5ba4-4449-a0a8-341a8c20a903"), new Guid("a43b7ac4-21e1-4afb-8b6c-a5a3a23edbea"), new DateTime(2025, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2099, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.1f, false, "Welcome" });

            migrationBuilder.InsertData(
                table: "ProductDetails",
                columns: new[] { "Id", "BeforePrice", "Description", "Price", "ProductId", "Stock", "TotalRating" },
                values: new object[] { new Guid("90356543-a5d9-433b-afb6-f2abb997487c"), 3285000f, "Description of product", 2950000f, new Guid("006b0864-fa5f-4657-8bd9-24ca15ed82ff"), 50, 0f });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedOn", "ImageId", "Name", "ThumbnailId" },
                values: new object[,]
                {
                    { new Guid("0830baab-2bb5-4fab-9566-a5c71db6dc0f"), new Guid("442f967f-1aa0-4aeb-8d35-c94fea58c98f"), new DateTime(2025, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Samsung A30", null },
                    { new Guid("1565a344-d40c-4284-aaa5-f084678ff799"), new Guid("442f967f-1aa0-4aeb-8d35-c94fea58c98f"), new DateTime(2025, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Xiaomi redmi note 10", null },
                    { new Guid("1a5c94ac-43d3-42a5-b5d6-1fc7ee40ca3a"), new Guid("442f967f-1aa0-4aeb-8d35-c94fea58c98f"), new DateTime(2025, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Xiaomi redmi note 8", null },
                    { new Guid("32f2ea2a-9f1d-4d0f-9b62-42df41d9060c"), new Guid("442f967f-1aa0-4aeb-8d35-c94fea58c98f"), new DateTime(2025, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Vivo 2", null },
                    { new Guid("33af0787-75a0-4ad5-87db-1641cf490c02"), new Guid("0241b743-ca16-4c25-96da-a2cf73dc008e"), new DateTime(2025, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Laptop Lenovo IdeaPad 15 Ultra 7", null },
                    { new Guid("3a1bb49d-401e-45d3-a907-74b0288fee2b"), new Guid("442f967f-1aa0-4aeb-8d35-c94fea58c98f"), new DateTime(2025, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "iPad 10 WiFi", null },
                    { new Guid("3a986b6e-b2ca-40f5-9f42-6dd84dc5291f"), new Guid("b0326961-b2c9-4681-8e36-d892cc1ca7ed"), new DateTime(2025, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "TWS JBL Wave Beam 1", null },
                    { new Guid("5c0a46c5-d34c-4cb5-8111-1ffbf1de055f"), new Guid("442f967f-1aa0-4aeb-8d35-c94fea58c98f"), new DateTime(2025, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "iPhone X 256GB", null },
                    { new Guid("5fb7c7ae-d5ff-4c2b-a60b-91bed322d975"), new Guid("442f967f-1aa0-4aeb-8d35-c94fea58c98f"), new DateTime(2025, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Vivo 3", null },
                    { new Guid("6623e124-e536-412c-93bc-17c7e8980e35"), new Guid("442f967f-1aa0-4aeb-8d35-c94fea58c98f"), new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Xiaomi redmi note 11", null },
                    { new Guid("6b41897f-b2f7-49ff-8dce-a9f8a84379f4"), new Guid("0241b743-ca16-4c25-96da-a2cf73dc008e"), new DateTime(2024, 11, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Laptop Lenovo ThinkBook 14 Ultra 7", null },
                    { new Guid("7dc7e323-bb50-461f-b888-7a292b9f0470"), new Guid("442f967f-1aa0-4aeb-8d35-c94fea58c98f"), new DateTime(2025, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "iPhone 8", null },
                    { new Guid("7f825066-55ed-4b5e-bc6e-006c516c31f0"), new Guid("0241b743-ca16-4c25-96da-a2cf73dc008e"), new DateTime(2025, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Laptop Apple MacBook Air 13", null },
                    { new Guid("982b3ca3-e5f1-4970-a958-41e8de10b2be"), new Guid("442f967f-1aa0-4aeb-8d35-c94fea58c98f"), new DateTime(2025, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "iPad Air 6 WiFi", null },
                    { new Guid("a1c679a8-a8e9-4243-9302-877193167ef2"), new Guid("0241b743-ca16-4c25-96da-a2cf73dc008e"), new DateTime(2025, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Laptop HP 15", null },
                    { new Guid("c4bebabe-f85a-4931-8230-777a74ff3f1e"), new Guid("442f967f-1aa0-4aeb-8d35-c94fea58c98f"), new DateTime(2025, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Vivo 1", null },
                    { new Guid("dae33944-cd29-4d3e-a52f-71d43f7306e8"), new Guid("0241b743-ca16-4c25-96da-a2cf73dc008e"), new DateTime(2025, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Laptop Lenovo ThinkBook 15 Ultra 7", null },
                    { new Guid("e56e3e30-1aac-4d83-8cf7-5e47c7a796e2"), new Guid("442f967f-1aa0-4aeb-8d35-c94fea58c98f"), new DateTime(2025, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Xiaomi redmi note 7", null },
                    { new Guid("e7000a32-e887-4246-a6dc-1988f2d66959"), new Guid("b0326961-b2c9-4681-8e36-d892cc1ca7ed"), new DateTime(2025, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "TWS JBL Wave Beam 2", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AvatarId", "CreatedOn", "Email", "Gender", "Name", "Password", "Phone", "RoleId" },
                values: new object[] { new Guid("e9b120bf-5e4b-453e-8c8a-423b8872ece3"), null, null, new DateTime(2025, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", null, "Admin", "$2a$11$3DnMnL3JrizdEtWpgg5ut.rp0jkJrUSlRyLbYBZpA94DYfSYFkJLa", null, new Guid("c26b7fcb-9e16-47aa-893e-3ef148de9714") });

            migrationBuilder.InsertData(
                table: "ProductDetails",
                columns: new[] { "Id", "BeforePrice", "Description", "Price", "ProductId", "Stock", "TotalRating" },
                values: new object[,]
                {
                    { new Guid("1cf25c81-65fc-45cf-ba25-23840ea74470"), 1500000f, "Description of product", 1000000f, new Guid("5fb7c7ae-d5ff-4c2b-a60b-91bed322d975"), 50, 0f },
                    { new Guid("1d609039-2fc2-498a-88ec-e2240a68a3d4"), 2150000f, "Description of product", 2000000f, new Guid("1a5c94ac-43d3-42a5-b5d6-1fc7ee40ca3a"), 100, 0f },
                    { new Guid("2034d5b0-b7f5-4832-a7cf-460e78d7065a"), 26000000f, "Description of product", 2150000f, new Guid("7f825066-55ed-4b5e-bc6e-006c516c31f0"), 200, 0f },
                    { new Guid("214b0870-5d51-4cf3-8c19-0c920a1c1c0f"), 1850000f, "Description of product", 950000f, new Guid("e56e3e30-1aac-4d83-8cf7-5e47c7a796e2"), 30, 0f },
                    { new Guid("596a66a7-0b96-48cd-a33c-1f064626d232"), 5100000f, "Description of product", 5000000f, new Guid("6623e124-e536-412c-93bc-17c7e8980e35"), 50, 0f },
                    { new Guid("66911a47-4c59-48ea-a5d1-865ef31c3239"), 11000000f, "Description of product", 8500000f, new Guid("3a986b6e-b2ca-40f5-9f42-6dd84dc5291f"), 100, 0f },
                    { new Guid("7a339b8c-b088-475a-9a38-d566a0c0ae33"), 21500000f, "Description of product", 20500000f, new Guid("32f2ea2a-9f1d-4d0f-9b62-42df41d9060c"), 50, 0f },
                    { new Guid("864f3b52-4c9f-45a0-8ef1-77001da42d8d"), 2400000f, "Description of product", 2150000f, new Guid("a1c679a8-a8e9-4243-9302-877193167ef2"), 200, 0f },
                    { new Guid("95cd6dee-9321-445e-8b07-6a21fa636e88"), 2850000f, "Description of product", 1950000f, new Guid("0830baab-2bb5-4fab-9566-a5c71db6dc0f"), 130, 0f },
                    { new Guid("a5523622-b1af-4c46-bdba-a6d7e81bb77a"), 11000000f, "Description of product", 8500000f, new Guid("e7000a32-e887-4246-a6dc-1988f2d66959"), 100, 0f },
                    { new Guid("b73adcad-88d7-4b61-9336-761d6d1b62c0"), 2400000f, "Description of product", 2150000f, new Guid("33af0787-75a0-4ad5-87db-1641cf490c02"), 200, 0f },
                    { new Guid("b75bb959-2e2e-4b72-8e24-1413684b74b8"), 11000000f, "Description of product", 8500000f, new Guid("3a1bb49d-401e-45d3-a907-74b0288fee2b"), 100, 0f },
                    { new Guid("ba35cb9e-f12e-420a-bf4c-0879197623a8"), 24000000f, "Description of product", 21500000f, new Guid("dae33944-cd29-4d3e-a52f-71d43f7306e8"), 20, 0f },
                    { new Guid("c1676b8b-c54b-4ba6-9b0d-1dee242c9e3b"), 25000000f, "Description of product", 24500000f, new Guid("6b41897f-b2f7-49ff-8dce-a9f8a84379f4"), 50, 0f },
                    { new Guid("c4b1ec49-1a63-478d-a7f2-5c88eca59d3a"), 13850000f, "Description of product", 8500000f, new Guid("7dc7e323-bb50-461f-b888-7a292b9f0470"), 150, 0f },
                    { new Guid("c7ee18b0-f1df-4540-aa8e-da8e3daf83f8"), 5000000f, "Description of product", 4500000f, new Guid("1565a344-d40c-4284-aaa5-f084678ff799"), 100, 0f },
                    { new Guid("e65ad7b9-3057-4076-9f83-9fd188843060"), 11000000f, "Description of product", 8500000f, new Guid("982b3ca3-e5f1-4970-a958-41e8de10b2be"), 100, 0f },
                    { new Guid("e95fa4eb-21e8-4c25-b8b1-4a3683c736c9"), 23850000f, "Description of product", 20850000f, new Guid("5c0a46c5-d34c-4cb5-8111-1ffbf1de055f"), 50, 0f },
                    { new Guid("eb72a97a-6b26-4ca9-bcb4-970b8478dbfc"), 11500000f, "Description of product", 10500000f, new Guid("c4bebabe-f85a-4931-8230-777a74ff3f1e"), 50, 0f }
                });

            migrationBuilder.InsertData(
                table: "Previews",
                columns: new[] { "Id", "ImageId", "ProductDetailId" },
                values: new object[,]
                {
                    { new Guid("0189ef5b-5bc1-4648-8d3b-71053faf3741"), null, new Guid("b73adcad-88d7-4b61-9336-761d6d1b62c0") },
                    { new Guid("0c756696-a404-489a-b7cd-7bf2ba612fa8"), null, new Guid("e65ad7b9-3057-4076-9f83-9fd188843060") },
                    { new Guid("0cc23b1e-2e69-4b55-ae6b-01d58db049ca"), null, new Guid("2034d5b0-b7f5-4832-a7cf-460e78d7065a") },
                    { new Guid("0f42be38-d4c4-4432-97fe-9216b2684a76"), null, new Guid("b75bb959-2e2e-4b72-8e24-1413684b74b8") },
                    { new Guid("1925f384-c0c1-4192-87a2-3779bdb778dd"), null, new Guid("864f3b52-4c9f-45a0-8ef1-77001da42d8d") },
                    { new Guid("19d6845e-7bef-4d1d-b82f-82d5cebe1322"), null, new Guid("864f3b52-4c9f-45a0-8ef1-77001da42d8d") },
                    { new Guid("35c17127-ef28-43c7-ba5b-b048da5727ec"), null, new Guid("66911a47-4c59-48ea-a5d1-865ef31c3239") },
                    { new Guid("381becd2-49f8-4dac-b8dc-71dd539cdbc0"), null, new Guid("c1676b8b-c54b-4ba6-9b0d-1dee242c9e3b") },
                    { new Guid("393d5edc-abea-453e-9067-81e4c82e4998"), null, new Guid("1d609039-2fc2-498a-88ec-e2240a68a3d4") },
                    { new Guid("440a00a1-9f0b-499b-b97a-c86bbed1a157"), null, new Guid("a5523622-b1af-4c46-bdba-a6d7e81bb77a") },
                    { new Guid("47dce916-9bfd-4af7-8b89-14a370637157"), null, new Guid("596a66a7-0b96-48cd-a33c-1f064626d232") },
                    { new Guid("4c193f4e-2a62-4d4a-847e-6ec878328bcc"), null, new Guid("c7ee18b0-f1df-4540-aa8e-da8e3daf83f8") },
                    { new Guid("539665e7-4c24-4b21-8db2-4468852c5ec6"), null, new Guid("b75bb959-2e2e-4b72-8e24-1413684b74b8") },
                    { new Guid("7326c5cb-f779-426a-9642-b70657c4bc44"), null, new Guid("b73adcad-88d7-4b61-9336-761d6d1b62c0") },
                    { new Guid("7623b9a1-bf62-42ba-8be8-1aec31053a20"), null, new Guid("864f3b52-4c9f-45a0-8ef1-77001da42d8d") },
                    { new Guid("7d30c313-2d62-43d7-b119-1221fce6ede7"), null, new Guid("e65ad7b9-3057-4076-9f83-9fd188843060") },
                    { new Guid("871abd86-41ee-40d9-aed0-09097db8d978"), null, new Guid("2034d5b0-b7f5-4832-a7cf-460e78d7065a") },
                    { new Guid("a97fd080-0c35-4d28-906e-96e6640a4695"), null, new Guid("ba35cb9e-f12e-420a-bf4c-0879197623a8") },
                    { new Guid("ade17c19-e768-4d19-b1d9-280a8a21dd05"), null, new Guid("ba35cb9e-f12e-420a-bf4c-0879197623a8") },
                    { new Guid("b01229c8-45e4-4c7a-985c-0926199d15f2"), null, new Guid("596a66a7-0b96-48cd-a33c-1f064626d232") },
                    { new Guid("b1fecedb-da85-43ca-bab3-feb345654797"), null, new Guid("c7ee18b0-f1df-4540-aa8e-da8e3daf83f8") },
                    { new Guid("b310f17b-5989-4d6a-8611-59ce810d5fa3"), null, new Guid("1d609039-2fc2-498a-88ec-e2240a68a3d4") },
                    { new Guid("b88a35e1-ce56-4529-b393-8bcc75a076d8"), null, new Guid("1d609039-2fc2-498a-88ec-e2240a68a3d4") },
                    { new Guid("bb3b3b23-e9fd-4f4b-bf7a-8a8afdec27e6"), null, new Guid("1d609039-2fc2-498a-88ec-e2240a68a3d4") },
                    { new Guid("bd9f34f0-50d8-4528-b69f-e9fcc527a21f"), null, new Guid("c1676b8b-c54b-4ba6-9b0d-1dee242c9e3b") },
                    { new Guid("cb9af524-f2e7-4b2a-9f5a-d2bfd8e9a55c"), null, new Guid("66911a47-4c59-48ea-a5d1-865ef31c3239") },
                    { new Guid("ea02a621-8c84-4721-8076-266d916a61c0"), null, new Guid("864f3b52-4c9f-45a0-8ef1-77001da42d8d") },
                    { new Guid("ffdbfd00-fa81-4319-b42a-c7a166ee0c70"), null, new Guid("a5523622-b1af-4c46-bdba-a6d7e81bb77a") }
                });

            migrationBuilder.InsertData(
                table: "ProductOptions",
                columns: new[] { "Id", "Name", "ProductDetailId" },
                values: new object[,]
                {
                    { new Guid("0d978831-f446-47d1-9ea4-d9349bf5b73b"), "Black", new Guid("596a66a7-0b96-48cd-a33c-1f064626d232") },
                    { new Guid("679c2c40-15ee-4898-aecb-ca318b024b74"), "White", new Guid("596a66a7-0b96-48cd-a33c-1f064626d232") },
                    { new Guid("76fdfcef-78b0-420f-97b0-4fd08c247e67"), "Orange", new Guid("c1676b8b-c54b-4ba6-9b0d-1dee242c9e3b") },
                    { new Guid("82cfc378-c2ae-4c8b-89b2-625bfc445fd5"), "Yellow", new Guid("ba35cb9e-f12e-420a-bf4c-0879197623a8") },
                    { new Guid("85652863-e101-43d4-bae2-f0fe52323b0e"), "Red", new Guid("c1676b8b-c54b-4ba6-9b0d-1dee242c9e3b") },
                    { new Guid("d0f9d23b-5312-4e10-8e3e-2c2739f17352"), "Original", new Guid("2034d5b0-b7f5-4832-a7cf-460e78d7065a") },
                    { new Guid("df069bcf-33d3-4a40-99d5-a286c8921ec3"), "Green", new Guid("ba35cb9e-f12e-420a-bf4c-0879197623a8") },
                    { new Guid("e4e47c49-e7ff-4496-9028-9c7617f5c08e"), "Blue", new Guid("c7ee18b0-f1df-4540-aa8e-da8e3daf83f8") },
                    { new Guid("f4c2a916-3629-49b2-9d7e-a827a3ee54eb"), "Classic", new Guid("2034d5b0-b7f5-4832-a7cf-460e78d7065a") },
                    { new Guid("fcf2efa8-321e-4a7b-a915-7f80017b2c64"), "Red", new Guid("c7ee18b0-f1df-4540-aa8e-da8e3daf83f8") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_OrderId",
                table: "CartItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductOptionId",
                table: "CartItems",
                column: "ProductOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BuyerId",
                table: "Orders",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderTypeId",
                table: "Orders",
                column: "OrderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_VoucherId",
                table: "Orders",
                column: "VoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_Previews_ImageId",
                table: "Previews",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Previews_ProductDetailId",
                table: "Previews",
                column: "ProductDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_ProductId",
                table: "ProductDetails",
                column: "ProductId",
                unique: true,
                filter: "[ProductId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOptions_ProductDetailId",
                table: "ProductOptions",
                column: "ProductDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ThumbnailId",
                table: "Products",
                column: "ThumbnailId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProductDetailId",
                table: "Reviews",
                column: "ProductDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_AvatarId",
                table: "Users",
                column: "AvatarId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Previews");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ProductOptions");

            migrationBuilder.DropTable(
                name: "OrderTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Vouchers");

            migrationBuilder.DropTable(
                name: "ProductDetails");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Images");
        }
    }
}
