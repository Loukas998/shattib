using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities.Products;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories;

public class SpecificationRepository(TemplateDbContext dbContext) : ISpecificationRepository
{
    public async Task<int> AddAttribute(Specification entity)
    {
        dbContext.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

	public async Task AddProductAttribute(List<ProductSpecification> entities)
	{
		dbContext.Productspecifications.AddRange(entities);
        await dbContext.SaveChangesAsync();
	}

	public async Task DeleteAttribute(Specification entity)
    {
        dbContext.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Specification>> GetAllAttributes()
    {
        return await dbContext.Specifications.ToListAsync();
    }

    public async Task UpdateAttribute(int id, string newName)
    {
        var specification = await dbContext.Specifications.FirstOrDefaultAsync(x => x.Id == id);
        specification!.Name = newName;
        await dbContext.SaveChangesAsync();
    }
}