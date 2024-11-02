using Template.Domain.Entities.Orders;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories
{
	public class OrderRepository(TemplateDbContext dbContext) : IOrderRepository
	{
		public async Task<int> CreateOrderAsync(Order entity)
		{
			dbContext.Orders.Add(entity);
			await dbContext.SaveChangesAsync();
			return entity.Id;
		}

		public Task DeleteOrderAsync(Order entity)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Order>> GetAllOrders()
		{
			throw new NotImplementedException();
		}

		public Task<Order> GetOrderById(int id)
		{
			throw new NotImplementedException();
		}

		public Task SaveChangesAsync()
		{
			throw new NotImplementedException();
		}
	}
}
