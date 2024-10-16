
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities;

namespace Template.Infrastructure.Persistence;

internal class TemplateDbContext(DbContextOptions<TemplateDbContext> options) : IdentityDbContext<User>(options)
{
	//internal DbSet<EntityType> table_name {get; set;}
	internal DbSet<Category> categories { get; set; }
	internal DbSet<SubCategory> subcategories { get; set; }
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		//relationships between the tables
		modelBuilder.Entity<Category>()
			.HasMany(c => c.SubCategories)
			.WithOne()
			.HasForeignKey(sc => sc.CategoryId);
	}
}
