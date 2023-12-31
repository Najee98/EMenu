﻿// <auto-generated />
using System;
using EMenu.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EMenu.Repositories.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230908140227_ModelsToDatabase")]
    partial class ModelsToDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("EMenu.Models.Models.Attribute", b =>
                {
                    b.Property<int>("attributeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("attributeName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("attributeId");

                    b.ToTable("Attributes");
                });

            modelBuilder.Entity("EMenu.Models.Models.Category", b =>
                {
                    b.Property<int>("categoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("categoryDescription")
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("categoryName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("categoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("EMenu.Models.Models.Image", b =>
                {
                    b.Property<int>("imageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("imageURL")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("imageId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("EMenu.Models.Models.Product", b =>
                {
                    b.Property<int>("productId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("imageId")
                        .HasColumnType("int");

                    b.Property<string>("productDescription")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("productImageId")
                        .HasColumnType("int");

                    b.Property<string>("productName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double?>("productPrice")
                        .HasColumnType("double");

                    b.HasKey("productId");

                    b.HasIndex("imageId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("EMenu.Models.Models.ProductVariant", b =>
                {
                    b.Property<int>("productVariantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("attributeId")
                        .HasColumnType("int");

                    b.Property<int>("prodctId")
                        .HasColumnType("int");

                    b.Property<int>("productId")
                        .HasColumnType("int");

                    b.Property<DateTime>("variantCreationDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("productVariantId");

                    b.HasIndex("attributeId");

                    b.HasIndex("productId");

                    b.ToTable("ProductVariants");
                });

            modelBuilder.Entity("EMenu.Models.Models.VariantAttribute", b =>
                {
                    b.Property<int>("productVariantId")
                        .HasColumnType("int");

                    b.Property<int>("attributeId")
                        .HasColumnType("int");

                    b.Property<string>("attributeDescription")
                        .HasColumnType("varchar(255)");

                    b.HasKey("productVariantId", "attributeId", "attributeDescription");

                    b.HasIndex("attributeId");

                    b.ToTable("VariantAttributes");
                });

            modelBuilder.Entity("EMenu.Models.Models.VariantImage", b =>
                {
                    b.Property<int>("productVariantId")
                        .HasColumnType("int");

                    b.Property<int>("imageId")
                        .HasColumnType("int");

                    b.HasKey("productVariantId", "imageId");

                    b.HasIndex("imageId");

                    b.ToTable("VariantImages");
                });

            modelBuilder.Entity("EMenu.Models.Models.Product", b =>
                {
                    b.HasOne("EMenu.Models.Models.Image", null)
                        .WithMany("products")
                        .HasForeignKey("imageId");
                });

            modelBuilder.Entity("EMenu.Models.Models.ProductVariant", b =>
                {
                    b.HasOne("EMenu.Models.Models.Attribute", null)
                        .WithMany("variants")
                        .HasForeignKey("attributeId");

                    b.HasOne("EMenu.Models.Models.Product", "product")
                        .WithMany("variants")
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("product");
                });

            modelBuilder.Entity("EMenu.Models.Models.VariantAttribute", b =>
                {
                    b.HasOne("EMenu.Models.Models.Attribute", null)
                        .WithMany("variantAttributes")
                        .HasForeignKey("attributeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EMenu.Models.Models.ProductVariant", null)
                        .WithMany("variantAttributes")
                        .HasForeignKey("productVariantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EMenu.Models.Models.VariantImage", b =>
                {
                    b.HasOne("EMenu.Models.Models.Image", null)
                        .WithMany("variantImages")
                        .HasForeignKey("imageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EMenu.Models.Models.ProductVariant", null)
                        .WithMany("variantImages")
                        .HasForeignKey("productVariantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EMenu.Models.Models.Attribute", b =>
                {
                    b.Navigation("variantAttributes");

                    b.Navigation("variants");
                });

            modelBuilder.Entity("EMenu.Models.Models.Image", b =>
                {
                    b.Navigation("products");

                    b.Navigation("variantImages");
                });

            modelBuilder.Entity("EMenu.Models.Models.Product", b =>
                {
                    b.Navigation("variants");
                });

            modelBuilder.Entity("EMenu.Models.Models.ProductVariant", b =>
                {
                    b.Navigation("variantAttributes");

                    b.Navigation("variantImages");
                });
#pragma warning restore 612, 618
        }
    }
}
