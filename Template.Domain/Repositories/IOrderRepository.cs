using Template.Domain.Entities.Orders;

namespace Template.Domain.Repositories
{
	public interface IOrderRepository
	{
		public Task<int> CreateOrderAsync(Order entity);
		public Task<Order> GetOrderById(int id);
		public Task<IEnumerable<Order>> GetAllOrders();
		public Task SaveChangesAsync();
		public Task DeleteOrderAsync(Order entity);
	}
}
