﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities;
using Template.Domain.Entities.Criterias;
using Template.Domain.Entities.EngConsultation;
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
    internal DbSet<CriteriaBill> CriteriaBills { get; set; }

    internal DbSet<Order> Orders { get; set; }
    internal DbSet<OrderItem> OrderItems { get; set; }

    internal DbSet<Consultation> Consultations { get; set; }
    internal DbSet<ContactUs> Contacts { get; set; }

    internal DbSet<OneTimePassword> OneTimePasswords { get; set; }
    internal DbSet<SpecifiedMeasurement> SpecifiedMeasurements { get; set; }

    internal DbSet<Visit> Visits { get; set; }

    internal DbSet<Color> Colors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //relationships between the tables

        // Category -> SubCategory (One-to-Many)
        modelBuilder.Entity<Category>()
            .HasMany(c => c.SubCategories)
            .WithOne(sb => sb.Category)
            .HasForeignKey(sc => sc.CategoryId);

        // Category -> CriteriaItem (One-to-Many)
        modelBuilder.Entity<Category>()
            .HasMany(c => c.CriteriaItems)
            .WithOne(ci => ci.Category)
            .HasForeignKey(ci => ci.CategoryId);

        // SubCategory -> Products (One-to-Many)
        modelBuilder.Entity<SubCategory>()
            .HasMany(sc => sc.Products)
            .WithOne(p => p.SubCategory)
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

        // Criteria -> CriteriaItem (One-0oto-Many)
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
        modelBuilder.Entity<CriteriaBill>()
            .HasOne(i => i.Criteria)
            .WithMany(c => c.CriteriaBills)
            .HasForeignKey(i => i.CriteriaId);

        // Products <-> Orders (Many-to-Many)
        modelBuilder.Entity<Product>()
            .HasMany(p => p.Orders)
            .WithMany(o => o.Products)
            .UsingEntity<OrderItem>();

        // User -> Orders (One-to-Many)
        modelBuilder.Entity<User>()
            .HasMany(u => u.Orders)
            .WithOne(o => o.User)
            .HasForeignKey(o => o.UserId);

        // User -> Consultations (One-to-Many)
		modelBuilder.Entity<User>()
			.HasMany(u => u.Consultations)
			.WithOne(c => c.User)
			.HasForeignKey(c => c.UserId);

        // User -> Criterias (One-to-Many)
        modelBuilder.Entity<User>()
            .HasMany(u => u.Criterias)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId);

		// User -> SpecifiedMeasurements (One-to-Many)
		modelBuilder.Entity<User>()
            .HasMany(u => u.SpecifiedMeasurements)
            .WithOne(sm => sm.User)
            .HasForeignKey(c => c.UserId);

        // Product -> Colors (One-to-Many)
        modelBuilder.Entity<Product>()
            .HasMany(p => p.Colors)
            .WithOne(c => c.Product)
            .HasForeignKey(c => c.ProductId);

		// Product -> Colors (One-to-Many)
		modelBuilder.Entity<Product>()
            .HasMany(p => p.Measurements)
            .WithOne(m => m.Product)
            .HasForeignKey(m => m.ProductId);
	}
}