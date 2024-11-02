using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities.Criteria;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories;

public class CriteriaRepository(TemplateDbContext dbContext) : ICriteriaRepository
{
    public async Task<IEnumerable<Criteria>> GetAllAsync()
    {
        return await dbContext.Criterias.Include(criteria => criteria.CriteriaItems)
            .ThenInclude(criteriaItem => criteriaItem.Category).ToListAsync();
    }

    public async Task<Criteria?> GetByIdAsync(int id)
    {
        return await dbContext.Criterias.FirstOrDefaultAsync(criteria => criteria.Id == id);
    }

    public async Task<int> CreateCriteriaAsync(Criteria criteria)
    {
        dbContext.Criterias.Add(criteria);
        await dbContext.SaveChangesAsync();
        return criteria.Id;
    }
}