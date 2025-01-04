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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>(a => a.HasData(
                new Role{Id = Guid.Parse("c26b7fcb-9e16-47aa-893e-3ef148de9714"),Name = "Admin"}, 
                new Role{Id = Guid.Parse("f80eee5a-eefe-49c6-9a11-2e5b3804a71c"),Name = "Customer"}
            ));
            modelBuilder.Entity<User>(a => a.HasData(new {Id=Guid.Parse("e9b120bf-5e4b-453e-8c8a-423b8872ece3"), Email = "admin@gmail.com", Password="$2a$11$3DnMnL3JrizdEtWpgg5ut.rp0jkJrUSlRyLbYBZpA94DYfSYFkJLa", Name="Admin", RoleId=Guid.Parse("c26b7fcb-9e16-47aa-893e-3ef148de9714")}));
            modelBuilder.Entity<Order>().HasOne(e => e.User).WithMany(v => v.Orders).HasForeignKey(e => e.BuyerId);
            modelBuilder.Entity<ProductDetail>().HasOne(e => e.Product).WithOne(v => v.ProductDetail).HasForeignKey<ProductDetail>(c => c.ProductId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Preview>().HasOne(e => e.ProductDetail).WithMany(v => v.Previews).HasForeignKey(c => c.ProductDetailId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ProductOption>().HasOne(e => e.ProductDetail).WithMany(v => v.ProductOptions).HasForeignKey(c => c.ProductDetailId).OnDelete(DeleteBehavior.Cascade);
        }

        
    }
}