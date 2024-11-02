using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Users;
using Template.Domain.Entities.Orders;
using Template.Domain.Repositories;

namespace Template.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler(ILogger<CreateOrderCommandHandler> logger,
        IOrderRepository orderRepository, IMapper mapper, IUserContext userContext,
		IProductRepository productRepository) : IRequestHandler<CreateOrderCommand, int>
    {
        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating order: {@Order}", request);
            var order = mapper.Map<Order>(request);
            order.UserId = userContext.GetCurrentUser()!.Id;
            int orderId = await orderRepository.CreateOrderAsync(order);

			foreach (var item in request.Items)
			{
				logger.LogInformation("adding item: {@Item} to order with id: {Id}", item, order.Id);

				var product = await productRepository.GetProductByIdAsync(item.ProductId);

				float totalPriceForEachItem = 0;
				if (product != null)
				{
					totalPriceForEachItem = item.Quantity * product.Price;
				}

				order.TotalPrice = totalPriceForEachItem;

				var orderItem = new OrderItem
				{
					OrderId = orderId,
					ProductId = item.ProductId,
					Price = totalPriceForEachItem,
					Quantity = item.Quantity,
				};
				order.OrderItems.Add(orderItem);
			}
			await orderRepository.SaveChangesAsync();
			return orderId;
		}
    }
}
