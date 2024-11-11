using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities.Criterias;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories;

public class CriteriaRepository(TemplateDbContext dbContext) : ICriteriaRepository
{
    public async Task<IEnumerable<Criteria>> GetAllAsync(string status)
    {
        var query = dbContext.Criterias
            .Include(criteria => criteria.CriteriaItems)
            .ThenInclude(criteriaItem => criteriaItem.Category)
            .Include(c => c.User)
            .AsQueryable();

        if (string.IsNullOrEmpty(status))
        {
            return await query.ToListAsync();
        }

        return await query.Where(c => c.Status == status).ToListAsync();
    }

    public async Task<Criteria?> GetByIdAsync(int id)
    {
        return await dbContext.Criterias
            .Include(criteria => criteria.CriteriaItems)
            .ThenInclude(criteriaItem => criteriaItem.Category)
            .Include(criteria => criteria.Comments)
            .Include(criteria => criteria.CriteriaBills)
            .Include(c => c.User)
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

    public async Task<Criteria> UpdateCriteriaAsync(Criteria criteria)
    {
        dbContext.Criterias.Update(criteria);
        await dbContext.SaveChangesAsync();
        return criteria;
    }
}