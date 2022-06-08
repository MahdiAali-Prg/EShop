﻿// <auto-generated />
using System;
using EShop.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EShop.Web.Migrations
{
    [DbContext(typeof(EShopContext))]
    partial class EShopContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EShop.Data.Models.BlogAuthor", b =>
                {
                    b.Property<long>("BlogAuthorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(800)")
                        .HasMaxLength(800);

                    b.HasKey("BlogAuthorId");

                    b.ToTable("BlogAuthors");
                });

            modelBuilder.Entity("EShop.Data.Models.BlogCategory", b =>
                {
                    b.Property<long>("BlogCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<long?>("ParentId")
                        .HasColumnType("bigint");

                    b.HasKey("BlogCategoryId");

                    b.HasIndex("ParentId");

                    b.ToTable("BlogCategories");
                });

            modelBuilder.Entity("EShop.Data.Models.Brand", b =>
                {
                    b.Property<long>("BrandId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(1200)")
                        .HasMaxLength(1200);

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(70)")
                        .HasMaxLength(70);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(320)")
                        .HasMaxLength(320);

                    b.HasKey("BrandId");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("EShop.Data.Models.BlogCategory", b =>
                {
                    b.HasOne("EShop.Data.Models.BlogCategory", "Category")
                        .WithMany("BlogCategories")
                        .HasForeignKey("ParentId");
                });
#pragma warning restore 612, 618
        }
    }
}
