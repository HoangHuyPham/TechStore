using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Datas
{
    public class ApplicationContext(DbContextOptions options) : DbContext(options)
    {
        public required DbSet<Product> Products { get; set; }
        public required DbSet<ProductDetail> ProductDetails { get; set; }
        public required DbSet<Category> Categories { get; set; }
        public required DbSet<Preview> Previews { get; set; }
        public required DbSet<ProductOption> ProductOptions { get; set; }
        public required DbSet<User> Users { get; set; }
        public required DbSet<Role> Roles { get; set; }
        public required DbSet<Cart> Carts { get; set; }
        public required DbSet<CartItem> CartItems { get; set; }
        public required DbSet<Image> Images { get; set; } 
        public required DbSet<OrderType> OrderTypes { get; set; } 
        public required DbSet<Order> Orders { get; set; } 
        public required DbSet<Voucher> Vouchers { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Init roles (admin, customer)
            modelBuilder.Entity<Role>(a => a.HasData(
                new Role { Id = Guid.Parse("c26b7fcb-9e16-47aa-893e-3ef148de9714"), Name = "Admin" },
                new Role { Id = Guid.Parse("f80eee5a-eefe-49c6-9a11-2e5b3804a71c"), Name = "Customer" }
            ));

            // Init voucher
            modelBuilder.Entity<Voucher>(a => a.HasData(
                new Voucher { Id = Guid.Parse("86a42389-5ba4-4449-a0a8-341a8c20a903"), Name = "Welcome", Code = Guid.Parse("a43b7ac4-21e1-4afb-8b6c-a5a3a23edbea"), CreatedAt = DateTime.Parse("2025-01-06"), ExpiredAt = DateTime.Parse("2099-01-06"), Factor=0.1f, IsActive=false}
            ));

            // Init category samples
            modelBuilder.Entity<Category>(a => a.HasData(
               new Category { Id = Guid.Parse("442f967f-1aa0-4aeb-8d35-c94fea58c98f"), Name = "Smart Phone" },
               new Category { Id = Guid.Parse("54733eda-9e2f-4675-a25c-1e46a5d4a347"), Name = "Tablet" },
               new Category { Id = Guid.Parse("0241b743-ca16-4c25-96da-a2cf73dc008e"), Name = "Laptop" },
               new Category { Id = Guid.Parse("e75c26e7-ce66-4fd7-a55e-fee9eb20b3a4"), Name = "Smart Watch" },
               new Category { Id = Guid.Parse("b0326961-b2c9-4681-8e36-d892cc1ca7ed"), Name = "Other" }
            ));

            // Init order type samples
            modelBuilder.Entity<OrderType>(a => a.HasData(
                new OrderType{Id = Guid.Parse("4f235200-957f-4051-83fa-93f0b7d5e2d3"), Name = "confirmed"},
                new OrderType{Id = Guid.Parse("f7e5cbee-e5de-4a7d-b688-5e56fdf96573"), Name = "pending"},
                new OrderType{Id = Guid.Parse("afb8d8b7-4035-42aa-a157-03f56d67c314"), Name = "cancelled"}
            ));

            // Init product samples
            modelBuilder.Entity<Product>(a => a.HasData(
                new { Id = Guid.Parse("1565a344-d40c-4284-aaa5-f084678ff799"), Name = "Xiaomi redmi note 10", CategoryId = Guid.Parse("442f967f-1aa0-4aeb-8d35-c94fea58c98f"), CreatedOn = DateTime.Parse("2025-01-06") }
            ));

            modelBuilder.Entity<Product>(a => a.HasData(
                new { Id = Guid.Parse("6623e124-e536-412c-93bc-17c7e8980e35"), Name = "Xiaomi redmi note 11", CategoryId = Guid.Parse("442f967f-1aa0-4aeb-8d35-c94fea58c98f"), CreatedOn = DateTime.Parse("2024-01-06") }
            ));

            modelBuilder.Entity<Product>(a => a.HasData(
                new { Id = Guid.Parse("6b41897f-b2f7-49ff-8dce-a9f8a84379f4"), Name = "Laptop Lenovo ThinkBook 14 Ultra 7", CategoryId = Guid.Parse("0241b743-ca16-4c25-96da-a2cf73dc008e"), CreatedOn = DateTime.Parse("2024-11-06") }
            ));

            modelBuilder.Entity<Product>(a => a.HasData(
                new { Id = Guid.Parse("dae33944-cd29-4d3e-a52f-71d43f7306e8"), Name = "Laptop Lenovo ThinkBook 15 Ultra 7", CategoryId = Guid.Parse("0241b743-ca16-4c25-96da-a2cf73dc008e"), CreatedOn = DateTime.Parse("2025-01-06") }
            ));

            modelBuilder.Entity<Product>(a => a.HasData(
                new { Id = Guid.Parse("7f825066-55ed-4b5e-bc6e-006c516c31f0"), Name = "Laptop Apple MacBook Air 13", CategoryId = Guid.Parse("0241b743-ca16-4c25-96da-a2cf73dc008e"), CreatedOn = DateTime.Parse("2025-01-06") }
            ));

            modelBuilder.Entity<Product>(a => a.HasData(
                new { Id = Guid.Parse("33af0787-75a0-4ad5-87db-1641cf490c02"), Name = "Laptop Lenovo IdeaPad 15 Ultra 7", CategoryId = Guid.Parse("0241b743-ca16-4c25-96da-a2cf73dc008e"), CreatedOn = DateTime.Parse("2025-01-06") }
            ));

            modelBuilder.Entity<Product>(a => a.HasData(
                new { Id = Guid.Parse("a1c679a8-a8e9-4243-9302-877193167ef2"), Name = "Laptop HP 15", CategoryId = Guid.Parse("0241b743-ca16-4c25-96da-a2cf73dc008e"), CreatedOn = DateTime.Parse("2025-01-06") }
            ));

            modelBuilder.Entity<Product>(a => a.HasData(
                new { Id = Guid.Parse("3a1bb49d-401e-45d3-a907-74b0288fee2b"), Name = "iPad 10 WiFi", CategoryId = Guid.Parse("442f967f-1aa0-4aeb-8d35-c94fea58c98f"), CreatedOn = DateTime.Parse("2025-01-06") }
            ));

            modelBuilder.Entity<Product>(a => a.HasData(
                new { Id = Guid.Parse("982b3ca3-e5f1-4970-a958-41e8de10b2be"), Name = "iPad Air 6 WiFi", CategoryId = Guid.Parse("442f967f-1aa0-4aeb-8d35-c94fea58c98f"), CreatedOn = DateTime.Parse("2025-01-06") }
            ));

            modelBuilder.Entity<Product>(a => a.HasData(
                new { Id = Guid.Parse("e7000a32-e887-4246-a6dc-1988f2d66959"), Name = "TWS JBL Wave Beam 2", CategoryId = Guid.Parse("b0326961-b2c9-4681-8e36-d892cc1ca7ed"), CreatedOn = DateTime.Parse("2025-01-06") }
            ));

            modelBuilder.Entity<Product>(a => a.HasData(
                new { Id = Guid.Parse("3a986b6e-b2ca-40f5-9f42-6dd84dc5291f"), Name = "TWS JBL Wave Beam 1", CategoryId = Guid.Parse("b0326961-b2c9-4681-8e36-d892cc1ca7ed"), CreatedOn = DateTime.Parse("2025-01-06") }
            ));

            modelBuilder.Entity<Product>(a => a.HasData(
                new { Id = Guid.Parse("1a5c94ac-43d3-42a5-b5d6-1fc7ee40ca3a"), Name = "Xiaomi redmi note 8", CategoryId = Guid.Parse("442f967f-1aa0-4aeb-8d35-c94fea58c98f"), CreatedOn = DateTime.Parse("2025-01-06") }
            ));

            modelBuilder.Entity<Product>(a => a.HasData(
                new { Id = Guid.Parse("e56e3e30-1aac-4d83-8cf7-5e47c7a796e2"), Name = "Xiaomi redmi note 7", CategoryId = Guid.Parse("442f967f-1aa0-4aeb-8d35-c94fea58c98f"), CreatedOn = DateTime.Parse("2025-01-06") }
            ));

            modelBuilder.Entity<Product>(a => a.HasData(
                new { Id = Guid.Parse("0830baab-2bb5-4fab-9566-a5c71db6dc0f"), Name = "Samsung A30", CategoryId = Guid.Parse("442f967f-1aa0-4aeb-8d35-c94fea58c98f"), CreatedOn = DateTime.Parse("2025-01-06") }
            ));

            modelBuilder.Entity<Product>(a => a.HasData(
                new { Id = Guid.Parse("006b0864-fa5f-4657-8bd9-24ca15ed82ff"), Name = "Samsung A70", CreatedOn = DateTime.Parse("2025-01-06") }
            ));

            modelBuilder.Entity<Product>(a => a.HasData(
                new { Id = Guid.Parse("5c0a46c5-d34c-4cb5-8111-1ffbf1de055f"), Name = "iPhone X 256GB", CategoryId = Guid.Parse("442f967f-1aa0-4aeb-8d35-c94fea58c98f"), CreatedOn = DateTime.Parse("2025-01-06") }
            ));

            modelBuilder.Entity<Product>(a => a.HasData(
                new { Id = Guid.Parse("7dc7e323-bb50-461f-b888-7a292b9f0470"), Name = "iPhone 8", CategoryId = Guid.Parse("442f967f-1aa0-4aeb-8d35-c94fea58c98f"), CreatedOn = DateTime.Parse("2025-01-06") }
            ));

            modelBuilder.Entity<Product>(a => a.HasData(
                new { Id = Guid.Parse("c4bebabe-f85a-4931-8230-777a74ff3f1e"), Name = "Vivo 1", CategoryId = Guid.Parse("442f967f-1aa0-4aeb-8d35-c94fea58c98f"), CreatedOn = DateTime.Parse("2025-01-06") }
            ));

            modelBuilder.Entity<Product>(a => a.HasData(
                new { Id = Guid.Parse("32f2ea2a-9f1d-4d0f-9b62-42df41d9060c"), Name = "Vivo 2", CategoryId = Guid.Parse("442f967f-1aa0-4aeb-8d35-c94fea58c98f"), CreatedOn = DateTime.Parse("2025-01-06") }
            ));

            modelBuilder.Entity<Product>(a => a.HasData(
                new { Id = Guid.Parse("5fb7c7ae-d5ff-4c2b-a60b-91bed322d975"), Name = "Vivo 3", CategoryId = Guid.Parse("442f967f-1aa0-4aeb-8d35-c94fea58c98f"), CreatedOn = DateTime.Parse("2025-01-06") }
            ));

            // Init product detail samples
            modelBuilder.Entity<ProductDetail>(a => a.HasData(
                new
                {
                    Id = Guid.Parse("c7ee18b0-f1df-4540-aa8e-da8e3daf83f8"),
                    BeforePrice = 5000000f,
                    Price = 4500000f,
                    Description = "Description of product",
                    Stock = 100,
                    TotalRating = 0f,
                    ProductId = Guid.Parse("1565a344-d40c-4284-aaa5-f084678ff799")
                },
                new
                {
                    Id = Guid.Parse("596a66a7-0b96-48cd-a33c-1f064626d232"),
                    BeforePrice = 5100000f,
                    Price = 5000000f,
                    Description = "Description of product",
                    Stock = 50,
                    TotalRating = 0f,
                    ProductId = Guid.Parse("6623e124-e536-412c-93bc-17c7e8980e35")
                },
                new
                {
                    Id = Guid.Parse("c1676b8b-c54b-4ba6-9b0d-1dee242c9e3b"),
                    BeforePrice = 25000000f,
                    Price = 24500000f,
                    Description = "Description of product",
                    Stock = 50,
                    TotalRating = 0f,
                    ProductId = Guid.Parse("6b41897f-b2f7-49ff-8dce-a9f8a84379f4")
                },
                new
                {
                    Id = Guid.Parse("ba35cb9e-f12e-420a-bf4c-0879197623a8"),
                    BeforePrice = 24000000f,
                    Price = 21500000f,
                    Description = "Description of product",
                    Stock = 20,
                    TotalRating = 0f,
                    ProductId = Guid.Parse("dae33944-cd29-4d3e-a52f-71d43f7306e8")
                },
                new
                {
                    Id = Guid.Parse("2034d5b0-b7f5-4832-a7cf-460e78d7065a"),
                    BeforePrice = 26000000f,
                    Price = 2150000f,
                    Description = "Description of product",
                    Stock = 200,
                    TotalRating = 0f,
                    ProductId = Guid.Parse("7f825066-55ed-4b5e-bc6e-006c516c31f0")
                },
                new
                {
                    Id = Guid.Parse("b73adcad-88d7-4b61-9336-761d6d1b62c0"),
                    BeforePrice = 2400000f,
                    Price = 2150000f,
                    Description = "Description of product",
                    Stock = 200,
                    TotalRating = 0f,
                    ProductId = Guid.Parse("33af0787-75a0-4ad5-87db-1641cf490c02")
                },
                new
                {
                    Id = Guid.Parse("864f3b52-4c9f-45a0-8ef1-77001da42d8d"),
                    BeforePrice = 2400000f,
                    Price = 2150000f,
                    Description = "Description of product",
                    Stock = 200,
                    TotalRating = 0f,
                    ProductId = Guid.Parse("a1c679a8-a8e9-4243-9302-877193167ef2") //hp 15
                },
                new
                {
                    Id = Guid.Parse("b75bb959-2e2e-4b72-8e24-1413684b74b8"),
                    BeforePrice = 11000000f,
                    Price = 8500000f,
                    Description = "Description of product",
                    Stock = 100,
                    TotalRating = 0f,
                    ProductId = Guid.Parse("3a1bb49d-401e-45d3-a907-74b0288fee2b") // ipad 10
                },
                new
                {
                    Id = Guid.Parse("e65ad7b9-3057-4076-9f83-9fd188843060"),
                    BeforePrice = 11000000f,
                    Price = 8500000f,
                    Description = "Description of product",
                    Stock = 100,
                    TotalRating = 0f,
                    ProductId = Guid.Parse("982b3ca3-e5f1-4970-a958-41e8de10b2be") // ipad 6
                },
                new
                {
                    Id = Guid.Parse("a5523622-b1af-4c46-bdba-a6d7e81bb77a"),
                    BeforePrice = 11000000f,
                    Price = 8500000f,
                    Description = "Description of product",
                    Stock = 100,
                    TotalRating = 0f,
                    ProductId = Guid.Parse("e7000a32-e887-4246-a6dc-1988f2d66959") // beam 2
                },
                new
                {
                    Id = Guid.Parse("66911a47-4c59-48ea-a5d1-865ef31c3239"),
                    BeforePrice = 11000000f,
                    Price = 8500000f,
                    Description = "Description of product",
                    Stock = 100,
                    TotalRating = 0f,
                    ProductId = Guid.Parse("3a986b6e-b2ca-40f5-9f42-6dd84dc5291f") // beam 1
                },
                new
                {
                    Id = Guid.Parse("1d609039-2fc2-498a-88ec-e2240a68a3d4"),
                    BeforePrice = 2150000f,
                    Price = 2000000f,
                    Description = "Description of product",
                    Stock = 100,
                    TotalRating = 0f,
                    ProductId = Guid.Parse("1a5c94ac-43d3-42a5-b5d6-1fc7ee40ca3a") // note 8
                },
                new
                {
                    Id = Guid.Parse("214b0870-5d51-4cf3-8c19-0c920a1c1c0f"),
                    BeforePrice = 1850000f,
                    Price = 950000f,
                    Description = "Description of product",
                    Stock = 30,
                    TotalRating = 0f,
                    ProductId = Guid.Parse("e56e3e30-1aac-4d83-8cf7-5e47c7a796e2") // note 7
                },
                new
                {
                    Id = Guid.Parse("95cd6dee-9321-445e-8b07-6a21fa636e88"),
                    BeforePrice = 2850000f,
                    Price = 1950000f,
                    Description = "Description of product",
                    Stock = 130,
                    TotalRating = 0f,
                    ProductId = Guid.Parse("0830baab-2bb5-4fab-9566-a5c71db6dc0f") // a30
                },
                new
                {
                    Id = Guid.Parse("90356543-a5d9-433b-afb6-f2abb997487c"),
                    BeforePrice = 3285000f,
                    Price = 2950000f,
                    Description = "Description of product",
                    Stock = 50,
                    TotalRating = 0f,
                    ProductId = Guid.Parse("006b0864-fa5f-4657-8bd9-24ca15ed82ff") // a70
                },
                new
                {
                    Id = Guid.Parse("e95fa4eb-21e8-4c25-b8b1-4a3683c736c9"),
                    BeforePrice = 23850000f,
                    Price = 20850000f,
                    Description = "Description of product",
                    Stock = 50,
                    TotalRating = 0f,
                    ProductId = Guid.Parse("5c0a46c5-d34c-4cb5-8111-1ffbf1de055f") // x
                },
                new
                {
                    Id = Guid.Parse("c4b1ec49-1a63-478d-a7f2-5c88eca59d3a"),
                    BeforePrice = 13850000f,
                    Price = 8500000f,
                    Description = "Description of product",
                    Stock = 150,
                    TotalRating = 0f,
                    ProductId = Guid.Parse("7dc7e323-bb50-461f-b888-7a292b9f0470") // 8
                },
                new
                {
                    Id = Guid.Parse("eb72a97a-6b26-4ca9-bcb4-970b8478dbfc"),
                    BeforePrice = 11500000f,
                    Price = 10500000f,
                    Description = "Description of product",
                    Stock = 50,
                    TotalRating = 0f,
                    ProductId = Guid.Parse("c4bebabe-f85a-4931-8230-777a74ff3f1e") // v1
                },
                new
                {
                    Id = Guid.Parse("1cf25c81-65fc-45cf-ba25-23840ea74470"),
                    BeforePrice = 1500000f,
                    Price = 1000000f,
                    Description = "Description of product",
                    Stock = 50,
                    TotalRating = 0f,
                    ProductId = Guid.Parse("5fb7c7ae-d5ff-4c2b-a60b-91bed322d975")
                },
                new
                {
                    Id = Guid.Parse("7a339b8c-b088-475a-9a38-d566a0c0ae33"),
                    BeforePrice = 21500000f,
                    Price = 20500000f,
                    Description = "Description of product",
                    Stock = 50,
                    TotalRating = 0f,
                    ProductId = Guid.Parse("32f2ea2a-9f1d-4d0f-9b62-42df41d9060c")
                }
            ));

            modelBuilder.Entity<Preview>(a => a.HasData(
                new { Id = Guid.Parse("4c193f4e-2a62-4d4a-847e-6ec878328bcc"), ProductDetailId = Guid.Parse("c7ee18b0-f1df-4540-aa8e-da8e3daf83f8") },
                new { Id = Guid.Parse("b1fecedb-da85-43ca-bab3-feb345654797"), ProductDetailId = Guid.Parse("c7ee18b0-f1df-4540-aa8e-da8e3daf83f8") },

                new { Id = Guid.Parse("b01229c8-45e4-4c7a-985c-0926199d15f2"), ProductDetailId = Guid.Parse("596a66a7-0b96-48cd-a33c-1f064626d232") },
                new { Id = Guid.Parse("47dce916-9bfd-4af7-8b89-14a370637157"), ProductDetailId = Guid.Parse("596a66a7-0b96-48cd-a33c-1f064626d232") },

                new { Id = Guid.Parse("381becd2-49f8-4dac-b8dc-71dd539cdbc0"), ProductDetailId = Guid.Parse("c1676b8b-c54b-4ba6-9b0d-1dee242c9e3b") },
                new { Id = Guid.Parse("bd9f34f0-50d8-4528-b69f-e9fcc527a21f"), ProductDetailId = Guid.Parse("c1676b8b-c54b-4ba6-9b0d-1dee242c9e3b") },

                new { Id = Guid.Parse("a97fd080-0c35-4d28-906e-96e6640a4695"), ProductDetailId = Guid.Parse("ba35cb9e-f12e-420a-bf4c-0879197623a8") },
                new { Id = Guid.Parse("ade17c19-e768-4d19-b1d9-280a8a21dd05"), ProductDetailId = Guid.Parse("ba35cb9e-f12e-420a-bf4c-0879197623a8") },

                new { Id = Guid.Parse("871abd86-41ee-40d9-aed0-09097db8d978"), ProductDetailId = Guid.Parse("2034d5b0-b7f5-4832-a7cf-460e78d7065a") },
                new { Id = Guid.Parse("0cc23b1e-2e69-4b55-ae6b-01d58db049ca"), ProductDetailId = Guid.Parse("2034d5b0-b7f5-4832-a7cf-460e78d7065a") },

                //

                new { Id = Guid.Parse("ea02a621-8c84-4721-8076-266d916a61c0"), ProductDetailId = Guid.Parse("864f3b52-4c9f-45a0-8ef1-77001da42d8d") },
                new { Id = Guid.Parse("1925f384-c0c1-4192-87a2-3779bdb778dd"), ProductDetailId = Guid.Parse("864f3b52-4c9f-45a0-8ef1-77001da42d8d") },

                new { Id = Guid.Parse("7326c5cb-f779-426a-9642-b70657c4bc44"), ProductDetailId = Guid.Parse("b73adcad-88d7-4b61-9336-761d6d1b62c0") },
                new { Id = Guid.Parse("0189ef5b-5bc1-4648-8d3b-71053faf3741"), ProductDetailId = Guid.Parse("b73adcad-88d7-4b61-9336-761d6d1b62c0") },

                new { Id = Guid.Parse("7623b9a1-bf62-42ba-8be8-1aec31053a20"), ProductDetailId = Guid.Parse("864f3b52-4c9f-45a0-8ef1-77001da42d8d") },
                new { Id = Guid.Parse("19d6845e-7bef-4d1d-b82f-82d5cebe1322"), ProductDetailId = Guid.Parse("864f3b52-4c9f-45a0-8ef1-77001da42d8d") },

                new { Id = Guid.Parse("539665e7-4c24-4b21-8db2-4468852c5ec6"), ProductDetailId = Guid.Parse("b75bb959-2e2e-4b72-8e24-1413684b74b8") },
                new { Id = Guid.Parse("0f42be38-d4c4-4432-97fe-9216b2684a76"), ProductDetailId = Guid.Parse("b75bb959-2e2e-4b72-8e24-1413684b74b8") },

                new { Id = Guid.Parse("0c756696-a404-489a-b7cd-7bf2ba612fa8"), ProductDetailId = Guid.Parse("e65ad7b9-3057-4076-9f83-9fd188843060") },
                new { Id = Guid.Parse("7d30c313-2d62-43d7-b119-1221fce6ede7"), ProductDetailId = Guid.Parse("e65ad7b9-3057-4076-9f83-9fd188843060") },

                new { Id = Guid.Parse("440a00a1-9f0b-499b-b97a-c86bbed1a157"), ProductDetailId = Guid.Parse("a5523622-b1af-4c46-bdba-a6d7e81bb77a") },
                new { Id = Guid.Parse("ffdbfd00-fa81-4319-b42a-c7a166ee0c70"), ProductDetailId = Guid.Parse("a5523622-b1af-4c46-bdba-a6d7e81bb77a") },

                new { Id = Guid.Parse("cb9af524-f2e7-4b2a-9f5a-d2bfd8e9a55c"), ProductDetailId = Guid.Parse("66911a47-4c59-48ea-a5d1-865ef31c3239") },
                new { Id = Guid.Parse("35c17127-ef28-43c7-ba5b-b048da5727ec"), ProductDetailId = Guid.Parse("66911a47-4c59-48ea-a5d1-865ef31c3239") }, 

                new { Id = Guid.Parse("b88a35e1-ce56-4529-b393-8bcc75a076d8"), ProductDetailId = Guid.Parse("1d609039-2fc2-498a-88ec-e2240a68a3d4") },
                new { Id = Guid.Parse("b310f17b-5989-4d6a-8611-59ce810d5fa3"), ProductDetailId = Guid.Parse("1d609039-2fc2-498a-88ec-e2240a68a3d4") },

                new { Id = Guid.Parse("393d5edc-abea-453e-9067-81e4c82e4998"), ProductDetailId = Guid.Parse("1d609039-2fc2-498a-88ec-e2240a68a3d4") },
                new { Id = Guid.Parse("bb3b3b23-e9fd-4f4b-bf7a-8a8afdec27e6"), ProductDetailId = Guid.Parse("1d609039-2fc2-498a-88ec-e2240a68a3d4") } 
            ));

            modelBuilder.Entity<ProductOption>(a => a.HasData(
                new { Id = Guid.Parse("fcf2efa8-321e-4a7b-a915-7f80017b2c64"),  Name = "Red", ProductDetailId = Guid.Parse("c7ee18b0-f1df-4540-aa8e-da8e3daf83f8") },
                new { Id = Guid.Parse("e4e47c49-e7ff-4496-9028-9c7617f5c08e"), Name = "Blue", ProductDetailId = Guid.Parse("c7ee18b0-f1df-4540-aa8e-da8e3daf83f8") },

                new { Id = Guid.Parse("0d978831-f446-47d1-9ea4-d9349bf5b73b"), Name = "Black", ProductDetailId = Guid.Parse("596a66a7-0b96-48cd-a33c-1f064626d232") },
                new { Id = Guid.Parse("679c2c40-15ee-4898-aecb-ca318b024b74"), Name = "White", ProductDetailId = Guid.Parse("596a66a7-0b96-48cd-a33c-1f064626d232") },

                new { Id = Guid.Parse("76fdfcef-78b0-420f-97b0-4fd08c247e67"), Name = "Orange", ProductDetailId = Guid.Parse("c1676b8b-c54b-4ba6-9b0d-1dee242c9e3b") },
                new { Id = Guid.Parse("85652863-e101-43d4-bae2-f0fe52323b0e"), Name = "Red", ProductDetailId = Guid.Parse("c1676b8b-c54b-4ba6-9b0d-1dee242c9e3b") },

                new { Id = Guid.Parse("df069bcf-33d3-4a40-99d5-a286c8921ec3"), Name = "Green", ProductDetailId = Guid.Parse("ba35cb9e-f12e-420a-bf4c-0879197623a8") },
                new { Id = Guid.Parse("82cfc378-c2ae-4c8b-89b2-625bfc445fd5"), Name = "Yellow", ProductDetailId = Guid.Parse("ba35cb9e-f12e-420a-bf4c-0879197623a8") },

                new { Id = Guid.Parse("d0f9d23b-5312-4e10-8e3e-2c2739f17352"), Name = "Original", ProductDetailId = Guid.Parse("2034d5b0-b7f5-4832-a7cf-460e78d7065a") },
                new { Id = Guid.Parse("f4c2a916-3629-49b2-9d7e-a827a3ee54eb"), Name = "Classic", ProductDetailId = Guid.Parse("2034d5b0-b7f5-4832-a7cf-460e78d7065a") }
            ));


            modelBuilder.Entity<User>(a => a.HasData(new { Id = Guid.Parse("e9b120bf-5e4b-453e-8c8a-423b8872ece3"), Email = "admin@gmail.com", Password = "$2a$11$3DnMnL3JrizdEtWpgg5ut.rp0jkJrUSlRyLbYBZpA94DYfSYFkJLa", Name = "Admin", RoleId = Guid.Parse("c26b7fcb-9e16-47aa-893e-3ef148de9714"), CreatedOn = DateTime.Parse("2025-01-06") }));
            modelBuilder.Entity<User>().HasOne(e=>e.Cart).WithOne(v=>v.User).HasForeignKey<Cart>(c=>c.UserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Order>().HasOne(e => e.User).WithMany(v => v.Orders).HasForeignKey(e => e.BuyerId);
            modelBuilder.Entity<ProductDetail>().HasOne(e => e.Product).WithOne(v => v.ProductDetail).HasForeignKey<ProductDetail>(c => c.ProductId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Preview>().HasOne(e => e.ProductDetail).WithMany(v => v.Previews).HasForeignKey(c => c.ProductDetailId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ProductOption>().HasOne(e => e.ProductDetail).WithMany(v => v.ProductOptions).HasForeignKey(c => c.ProductDetailId).OnDelete(DeleteBehavior.Cascade);
        }


    }
}