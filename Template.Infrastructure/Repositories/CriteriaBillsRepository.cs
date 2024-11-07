using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities.Criteria;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories;

public class CriteriaBillsRepository(TemplateDbContext dbContext) : ICriteriaBillsRepository
{
    public async Task<List<CriteriaBill>> GetAllBillsAsync()
    {
        return await dbContext.CriteriaBills.ToListAsync();
    }

    public async Task<CriteriaBill?> GetBillByIdAsync(int id)
    {
        return await dbContext.CriteriaBills.FindAsync(id);
    }

    public async Task<CriteriaBill> UpdateBillAsync(CriteriaBill criteriaBill)
    {
        dbContext.CriteriaBills.Update(criteriaBill);
        await dbContext.SaveChangesAsync();
        return criteriaBill;
    }

    public async Task DeleteBillAsync(CriteriaBill criteriaBill)
    {
        dbContext.CriteriaBills.Remove(criteriaBill);
        await dbContext.SaveChangesAsync();
    }


    public async Task<CriteriaBill?> CreateReceiptAsync(int billId, string receiptPath)
    {
        var bill = await dbContext.CriteriaBills.FindAsync(billId);
        if (bill == null) return null;
        bill.Receipt = receiptPath;
        await dbContext.SaveChangesAsync();
        return bill;
    }

    public async Task<int> CreateBillAsync(CriteriaBill criteriaBill, CancellationToken ct)
    {
        dbContext.CriteriaBills.Add(criteriaBill);
        await dbContext.SaveChangesAsync(ct);
        return criteriaBill.Id;
    }

    public async Task<CriteriaBill?> UpdateBillAcceptedAsync(int billId, bool isAccepted)
    {
        var bill = await dbContext.CriteriaBills.FindAsync(billId);
        if (bill == null) return null;
        bill.Accepted = isAccepted;
        await dbContext.SaveChangesAsync();
        return bill;
    }
}