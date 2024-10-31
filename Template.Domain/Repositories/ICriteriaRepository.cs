using Template.Domain.Entities.Criteria;

namespace Template.Domain.Repositories;

public interface ICriteriaRepository
{
    public Task<IEnumerable<Criteria>> GetAllAsync();

    public Task<Criteria> GetByIdAsync(int id);

    public Task<int> CreateCriteriaAsync(Criteria criteria);

    
}