using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities;
using Template.Domain.Entities.Criteria;
using Template.Domain.Entities.Products;

namespace Template.Infrastructure.Persistence;

public class TemplateDbContext(DbContextOptions<TemplateDbContext> options) : IdentityDbContext<User>(options)
{
    //internal DbSet<EntityType> table_name {get; set;}
    internal DbSet<Category> Categories { get; set; }
    internal DbSet<SubCategory> Subcategories { get; set; }
    internal DbSet<Product> Products { get; set; }
    internal DbSet<ProductImages> ProductImages { get; set; }
    internal DbSet<Specification> Specifications { get; set; }
    internal DbSet<ProductSpecification> Productspecifications { get; set; }

    internal DbSet<Criteria> Criterias { get; set; }
    internal DbSet<CriteriaItem> CriteriaItems { get; set; }
    internal DbSet<CriteriaImages> CriteriaImages { get; set; }
    internal DbSet<Comment> Comments { get; set; }
    internal DbSet<Invoice> Invoices { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //relationships between the tables

        // Category -> SubCategory (One-to-Many)
        modelBuilder.Entity<Category>()
            .HasMany(c => c.SubCategories)
            .WithOne()
            .HasForeignKey(sc => sc.CategoryId);

        // Category -> CriteriaItem (One-to-Many)
        modelBuilder.Entity<Category>()
            .HasMany(c => c.CriteriaItems)
            .WithOne(ci => ci.Category)
            .HasForeignKey(ci => ci.CategoryId);

        // SubCategory -> Products (One-to-Many)
        modelBuilder.Entity<SubCategory>()
            .HasMany(sc => sc.Products)
            .WithOne()
            .HasForeignKey(p => p.SubCategoryId);

        // Product -> Images (One-to-Many)
        modelBuilder.Entity<Product>()
            .HasMany(p => p.Images)
            .WithOne()
            .HasForeignKey(i => i.ProductId);

        // Product -> Specifications (One-to-Many)
        modelBuilder.Entity<Product>()
            .HasMany(p => p.Specifications)
            .WithMany(s => s.Products)
            .UsingEntity<ProductSpecification>();

        // Criteria -> CriteriaItem (One-to-Many)
        modelBuilder.Entity<Criteria>()
            .HasMany(c => c.CriteriaItems)
            .WithOne(ci => ci.Criteria)
            .HasForeignKey(ci => ci.CriteriaId);

        // CriteriaItem -> CriteriaImages (One-to-Many)
        modelBuilder.Entity<CriteriaItem>()
            .HasMany(ci => ci.CriteriaImages)
            .WithOne(cimg => cimg.CriteriaItem)
            .HasForeignKey(cimg => cimg.CriteriaCategoryId);

        // Criteria -> Comments (One-to-Many)
        modelBuilder.Entity<Criteria>()
            .HasMany(c => c.Comments)
            .WithOne(comment => comment.Criteria)
            .HasForeignKey(comment => comment.CriteriaId);

        // Invoice -> Criteria (One-to-Many)
        modelBuilder.Entity<Invoice>()
            .HasOne(i => i.Criteria)
            .WithMany(c => c.Invoices)
            .HasForeignKey(i => i.CriteriaId);
    }
}