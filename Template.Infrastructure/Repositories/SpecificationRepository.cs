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

	public async Task AddProductAttribute(ProductSpecification entity)
	{
		dbContext.Productspecifications.Add(entity);
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

    public async Task<Specification> GetAttributeById(int id)
    {
		var specification = await dbContext.Specifications.FirstOrDefaultAsync(x => x.Id == id);
        return specification;
	}

	public async Task<Specification> GetAttributeByName(string name)
	{
		var specification = await dbContext.Specifications.FirstOrDefaultAsync(x => x.Name == name);
		return specification;
	}
}