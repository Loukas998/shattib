using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities.Criteria;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories;

public class CriteriaRepository(TemplateDbContext dbContext) : ICriteriaRepository
{
    public async Task<IEnumerable<Criteria>> GetAllAsync()
    {
        return await dbContext.Criterias
            .Include(criteria => criteria.CriteriaItems)
            .ThenInclude(criteriaItem => criteriaItem.Category)
            .ToListAsync();
    }

    public async Task<Criteria?> GetByIdAsync(int id)
    {
        return await dbContext.Criterias
            .Include(criteria => criteria.CriteriaItems)
            .ThenInclude(criteriaItem => criteriaItem.Category)
            .Include(criteria => criteria.Comments)
            .Include(criteria => criteria.CriteriaBills)
            .FirstOrDefaultAsync(criteria => criteria.Id == id);
    }

    public async Task<int> CreateCriteriaAsync(Criteria criteria)
    {
        dbContext.Criterias.Add(criteria);
        await dbContext.SaveChangesAsync();
        return criteria.Id;
    }

    public async Task<List<Criteria>> GetAllByUserId(string userId)
    {
        return await dbContext.Criterias
            .Include(criteria => criteria.CriteriaItems)
            .ThenInclude(criteriaItem => criteriaItem.Category)
            .Include(criteria => criteria.Comments)
            .Include(criteria => criteria.CriteriaBills)
            .Where(criteria => criteria.UserId == userId)
            .ToListAsync();
    }

    public async Task<Criteria?> UpdateCriteriaStatusAsync(int id, string status)
    {
        var criteria = await dbContext.Criterias.FindAsync(id);
        if (criteria == null) return null;
        criteria.Status = status;
        await dbContext.SaveChangesAsync();
        return criteria;
    }
}