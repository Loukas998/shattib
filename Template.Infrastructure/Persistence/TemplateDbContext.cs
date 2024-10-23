
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities;
using Template.Domain.Entities.Products;

namespace Template.Infrastructure.Persistence;

public class TemplateDbContext(DbContextOptions<TemplateDbContext> options) : IdentityDbContext<User>(options)
{
	//internal DbSet<EntityType> table_name {get; set;}
	internal DbSet<Category> categories { get; set; }
	internal DbSet<SubCategory> subcategories { get; set; }
	internal DbSet<Product> products { get; set; }
	internal DbSet<ProductImages> ProductImages { get; set; }
	internal DbSet<Specification> specifications { get; set; }
	internal DbSet<ProductSpecification> productspecifications { get; set; }
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		//relationships between the tables
		modelBuilder.Entity<Category>()
			.HasMany(c => c.SubCategories)
			.WithOne()
			.HasForeignKey(sc => sc.CategoryId);

		modelBuilder.Entity<SubCategory>()
			.HasMany(sc => sc.Products)
			.WithOne()
			.HasForeignKey(p => p.SubCategoryId);

		modelBuilder.Entity<Product>()
			.HasMany(p => p.Images)
			.WithOne()
			.HasForeignKey(i => i.ProductId);

		modelBuilder.Entity<Product>()
			.HasMany(p => p.Specifications)
			.WithMany(s => s.Products)
			.UsingEntity<ProductSpecification>();
	}
}
