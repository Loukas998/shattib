using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities.Criteria;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories;

public class CriteriaBillsRepository(TemplateDbContext dbContext)
{
    public async Task<int> CreateBillAsync(CriteriaBills criteriaBill)
    {
        dbContext.CriteriaBills.Add(criteriaBill);
        await dbContext.SaveChangesAsync();
        return criteriaBill.Id;
    }

    public async Task<List<CriteriaBills>> GetAllBillsAsync()
    {
        return await dbContext.CriteriaBills.ToListAsync();
    }

    public async Task<CriteriaBills?> GetBillByIdAsync(int id)
    {
        return await dbContext.CriteriaBills.FindAsync(id);
    }

    public async Task<CriteriaBills> UpdateBillAsync(CriteriaBills criteriaBill)
    {
        dbContext.CriteriaBills.Update(criteriaBill);
        await dbContext.SaveChangesAsync();
        return criteriaBill;
    }

    public async Task DeleteBillAsync(CriteriaBills criteriaBill)
    {
        dbContext.CriteriaBills.Remove(criteriaBill);
    }
}