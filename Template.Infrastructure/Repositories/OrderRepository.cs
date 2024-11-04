using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using Template.Domain.Entities;
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

		public async Task DeleteOrderAsync(Order entity)
		{
			dbContext.Orders.Remove(entity);
			await dbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<Order>> GetAllOrders()
		{
			var orders = await dbContext.Orders.ToListAsync();
			return orders;
		}

		public async Task<IEnumerable<Order>> GetOrdersByKind(string kind)
		{
			return await dbContext.Orders.Where(o => o.Kind == kind).ToListAsync();
		}

		public async Task<IEnumerable<Order>> GetUserOrders(string userId)
		{
			return await dbContext.Orders.Where(o => o.UserId == userId).ToListAsync();
		}

		public async Task<IEnumerable<Order>> GetUserOrdersByKind(string userId, string kind)
		{
			return await dbContext.Orders.Where(o => o.UserId == userId)
										 .Where(o => o.Kind == kind)
										 .ToListAsync();
		}

		public async Task<Order?> GetOrderById(int id)
		{
			return await dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
		}

		//used dictionary for easy grouping  
		public async Task<Dictionary<int, List<DetailedOrderItemDto>>> GetOrderItemsForOrders(List<int> orderIds)
		{
			var orderItems = from oi in dbContext.OrderItems
							 join p in dbContext.Products.Include(p => p.Images)
								on oi.ProductId equals p.Id
							 where orderIds.Contains(oi.OrderId)
							 select new
							 {
								 oi.OrderId,
								 OrderItem = new DetailedOrderItemDto
								 {
									 ProductMainImage = p.Images.FirstOrDefault().ImagePath ?? string.Empty,
									 ProductName = p.Name,
									 Quantitiy = oi.Quantity,
									 TotalPriceForThisProduct = oi.Price,
								 }
							 };
			var itemsList = await orderItems.ToListAsync();

			// this to group by OrderId in a dict
			return itemsList.GroupBy(x => x.OrderId)
							 .ToDictionary(g => g.Key, g => g.Select(x => x.OrderItem).ToList());
			//hope you get it
		}

		public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();

		public async Task<IEnumerable<Order>> GetOrdersByStatus(string status)
		{
			var orders = await dbContext.Orders.Where(o => o.Status == status).ToListAsync();
			return orders;
		}

		public async Task<IEnumerable<Order>> GetUserOrdersByStatus(string status, string userId)
		{
			var orders = await dbContext.Orders.Where(o => o.Status == status)
											   .Where(o => o.UserId == userId)
											   .ToListAsync();
			return orders;
		}
	}
}


