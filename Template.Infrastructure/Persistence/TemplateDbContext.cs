using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities;
using Template.Domain.Entities.Criteria;
using Template.Domain.Entities.Orders;
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

    internal DbSet<Comment> Comments { get; set; }
    internal DbSet<CriteriaBills> Invoices { get; set; }

    internal DbSet<Order> Orders { get; set; }
    internal DbSet<OrderItem> OrderItems { get; set; }

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

        // Criteria -> Comments (One-to-Many)
        modelBuilder.Entity<Criteria>()
            .HasMany(c => c.Comments)
            .WithOne(comment => comment.Criteria)
            .HasForeignKey(comment => comment.CriteriaId);

        // Invoice -> Criteria (One-to-Many)
        modelBuilder.Entity<CriteriaBills>()
            .HasOne(i => i.Criteria)
            .WithMany(c => c.Invoices)
            .HasForeignKey(i => i.CriteriaId);

        // Products <-> Orders (Many-to-Many)
        modelBuilder.Entity<Product>()
            .HasMany(p => p.Orders)
            .WithMany(o => o.Products)
            .UsingEntity<OrderItem>();

        // User -> Orders (One-to-Many)
        modelBuilder.Entity<User>()
            .HasMany(u => u.Orders)
            .WithOne()
            .HasForeignKey(o => o.UserId);
    }
}