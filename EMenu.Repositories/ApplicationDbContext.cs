using EMenu.Models.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Attribute = EMenu.Models.Models.Attribute;

namespace EMenu.Repositories
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Attribute> Attributes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<VariantAttribute> VariantAttributes { get; set; }
        public DbSet<VariantImage> VariantImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VariantAttribute>()
                .HasKey(j => new { j.productVariantId, j.attributeId, j.attributeDescription });

            modelBuilder.Entity<VariantImage>()
                .HasKey(j => new {j.productVariantId, j.imageId});


        }

    }
}