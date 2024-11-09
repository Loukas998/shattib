using Template.Domain.Entities.Criterias;

namespace Template.Domain.Repositories;

public interface ICriteriaBillsRepository
{
    public Task<int> CreateBillAsync(CriteriaBill criteriaBill, CancellationToken ct);
    public Task<List<CriteriaBill>> GetAllBillsAsync();
    public Task<CriteriaBill?> GetBillByIdAsync(int id);
    public Task<CriteriaBill> UpdateBillAsync(CriteriaBill criteriaBill);
    public Task DeleteBillAsync(CriteriaBill criteriaBill);
    public Task<CriteriaBill?> UpdateBillAcceptedAsync(int billId, bool isAccepted);
    public Task<CriteriaBill?> CreateReceiptAsync(int billId, string receiptPath);
}