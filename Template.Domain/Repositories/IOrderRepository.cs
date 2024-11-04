using Template.Domain.Entities.Orders;

namespace Template.Domain.Repositories
{
	public interface IOrderRepository
	{
		public Task<int> CreateOrderAsync(Order entity);
		public Task<Order?> GetOrderById(int id);
		public Task<IEnumerable<Order>> GetAllOrders();
		public Task<IEnumerable<Order>> GetUserOrders(string userId);
		public Task<IEnumerable<Order>> GetUserOrdersByKind(string userId, string kind);
		public Task<IEnumerable<Order>> GetOrdersByKind(string kind);
		public Task<IEnumerable<Order>> GetOrdersByStatus(string status);
		public Task<IEnumerable<Order>> GetUserOrdersByStatus(string status, string userId);
		public Task SaveChangesAsync();
		public Task DeleteOrderAsync(Order entity);
		public Task<Dictionary<int, List<DetailedOrderItemDto>>> GetOrderItemsForOrders(List<int> orderIds);
	}
}
