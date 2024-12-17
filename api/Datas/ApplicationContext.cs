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
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Order>().HasOne(e=>e.User).WithMany(v=>v.Orders).HasForeignKey(e=>e.BuyerId);
            modelBuilder.Entity<ProductDetail>().HasOne(e=>e.Product).WithOne(v=>v.ProductDetail).HasForeignKey<ProductDetail>(c=>c.ProductId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Preview>().HasOne(e=>e.ProductDetail).WithMany(v=>v.Previews).HasForeignKey(c=>c.ProductDetailId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ProductOption>().HasOne(e=>e.ProductDetail).WithMany(v=>v.ProductOptions).HasForeignKey(c=>c.ProductDetailId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}