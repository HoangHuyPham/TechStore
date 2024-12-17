﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api.Datas;

#nullable disable

namespace api.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RoleUserGroup", b =>
                {
                    b.Property<Guid>("RolesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserGroupsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RolesId", "UserGroupsId");

                    b.HasIndex("UserGroupsId");

                    b.ToTable("RoleUserGroup");
                });

            modelBuilder.Entity("api.Models.Cart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("api.Models.CartItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CartId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsSelected")
                        .HasColumnType("bit");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<short>("Quantity")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("api.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("api.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BuyerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<Guid>("OrderTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("VoucherId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("OrderTypeId");

                    b.HasIndex("VoucherId")
                        .IsUnique()
                        .HasFilter("[VoucherId] IS NOT NULL");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("api.Models.OrderType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("OrderTypes");
                });

            modelBuilder.Entity("api.Models.Preview", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProductDetailId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("URL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductDetailId");

                    b.ToTable("Previews");
                });

            modelBuilder.Entity("api.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("Thumbnail")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("api.Models.ProductDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("BeforePrice")
                        .HasColumnType("real");

                    b.Property<string>("Description")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<float>("TotalRating")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("ProductId")
                        .IsUnique()
                        .HasFilter("[ProductId] IS NOT NULL");

                    b.ToTable("ProductDetails");
                });

            modelBuilder.Entity("api.Models.ProductOption", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<Guid?>("ProductDetailId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProductDetailId");

                    b.ToTable("ProductOptions");
                });

            modelBuilder.Entity("api.Models.Review", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ProductDetailId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Rating")
                        .HasColumnType("real");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProductDetailId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("api.Models.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("URL")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("api.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<int>("Phone")
                        .HasColumnType("int");

                    b.Property<Guid>("UserGroupId1")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserGroupId1");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("api.Models.UserGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.HasKey("Id");

                    b.ToTable("UserGroups");
                });

            modelBuilder.Entity("api.Models.Voucher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpiredAt")
                        .HasColumnType("datetime2");

                    b.Property<float>("Factor")
                        .HasColumnType("real");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.HasKey("Id");

                    b.ToTable("Vouchers");
                });

            modelBuilder.Entity("RoleUserGroup", b =>
                {
                    b.HasOne("api.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.UserGroup", null)
                        .WithMany()
                        .HasForeignKey("UserGroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("api.Models.Cart", b =>
                {
                    b.HasOne("api.Models.User", "User")
                        .WithMany("Carts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("api.Models.CartItem", b =>
                {
                    b.HasOne("api.Models.Cart", "Cart")
                        .WithMany("CartItems")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.Order", "Order")
                        .WithMany("CartItems")
                        .HasForeignKey("OrderId");

                    b.HasOne("api.Models.Product", "Product")
                        .WithMany("SelectedItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("api.Models.Order", b =>
                {
                    b.HasOne("api.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.OrderType", "OrderType")
                        .WithMany("Orders")
                        .HasForeignKey("OrderTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.Voucher", "Voucher")
                        .WithOne("Order")
                        .HasForeignKey("api.Models.Order", "VoucherId");

                    b.Navigation("OrderType");

                    b.Navigation("User");

                    b.Navigation("Voucher");
                });

            modelBuilder.Entity("api.Models.Preview", b =>
                {
                    b.HasOne("api.Models.ProductDetail", "ProductDetail")
                        .WithMany("Previews")
                        .HasForeignKey("ProductDetailId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("ProductDetail");
                });

            modelBuilder.Entity("api.Models.Product", b =>
                {
                    b.HasOne("api.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("api.Models.ProductDetail", b =>
                {
                    b.HasOne("api.Models.Product", "Product")
                        .WithOne("ProductDetail")
                        .HasForeignKey("api.Models.ProductDetail", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Product");
                });

            modelBuilder.Entity("api.Models.ProductOption", b =>
                {
                    b.HasOne("api.Models.ProductDetail", "ProductDetail")
                        .WithMany("ProductOptions")
                        .HasForeignKey("ProductDetailId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("ProductDetail");
                });

            modelBuilder.Entity("api.Models.Review", b =>
                {
                    b.HasOne("api.Models.ProductDetail", "ProductDetail")
                        .WithMany()
                        .HasForeignKey("ProductDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.User", "User")
                        .WithOne("Review")
                        .HasForeignKey("api.Models.Review", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductDetail");

                    b.Navigation("User");
                });

            modelBuilder.Entity("api.Models.User", b =>
                {
                    b.HasOne("api.Models.UserGroup", "UserGroup")
                        .WithMany("Users")
                        .HasForeignKey("UserGroupId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserGroup");
                });

            modelBuilder.Entity("api.Models.Cart", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("api.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("api.Models.Order", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("api.Models.OrderType", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("api.Models.Product", b =>
                {
                    b.Navigation("ProductDetail")
                        .IsRequired();

                    b.Navigation("SelectedItems");
                });

            modelBuilder.Entity("api.Models.ProductDetail", b =>
                {
                    b.Navigation("Previews");

                    b.Navigation("ProductOptions");
                });

            modelBuilder.Entity("api.Models.User", b =>
                {
                    b.Navigation("Carts");

                    b.Navigation("Orders");

                    b.Navigation("Review");
                });

            modelBuilder.Entity("api.Models.UserGroup", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("api.Models.Voucher", b =>
                {
                    b.Navigation("Order");
                });
#pragma warning restore 612, 618
        }
    }
}
