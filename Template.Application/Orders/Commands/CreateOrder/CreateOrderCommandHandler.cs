using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Users;
using Template.Domain.Constants;
using Template.Domain.Entities.Orders;
using Template.Domain.Entities.Products;
using Template.Domain.Exceptions;
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

			var currentUser = userContext.GetCurrentUser();
			if (currentUser == null) throw new UnauthorizedException("You are unauthorized.. login again (no userId)");
			var userId = currentUser.Id;

			order.UserId = userId;
			if (order.Kind == OrderConstants.Sample) order.TotalPrice = 0;
            int orderId = await orderRepository.CreateOrderAsync(order);

			foreach (var item in request.Items)
			{
				logger.LogInformation("adding item: {@Item} to order with id: {Id}", item, order.Id);

				var product = await productRepository.GetProductByIdAsync(item.ProductId);
				if (product == null)
				{
					throw new NotFoundException(nameof(Product), item.ProductId.ToString());
				}

				var color = product.Colors.FirstOrDefault(c => c.Id == item.ColorId);
				if (color == null)
				{
					throw new NotFoundException(nameof(Color), item.ColorId.ToString());
				}

				var measurement = product.Measurements.FirstOrDefault(m => m.Id == item.MeasurementId);
				if (measurement == null)
				{
					throw new NotFoundException(nameof(Measurement), item.MeasurementId.ToString());
				}

				float totalPriceForEachItem = 0;
				if (product != null && order.Kind == OrderConstants.Order)
				{
					totalPriceForEachItem = (item.Quantity * product.Price);
				}

				if (product != null && item.WithInstallation)
				{
					totalPriceForEachItem += product.InstallationTeam;
				}

				totalPriceForEachItem = measurement.Price + color.Price;

				order.TotalPrice = totalPriceForEachItem;

				var orderItem = new OrderItem
				{
					OrderId = orderId,
					ProductId = item.ProductId,
					Color = color.ImagePath,
					Price = totalPriceForEachItem,
					Quantity = item.Quantity,
					WithInstallation = item.WithInstallation
				};
				order.OrderItems.Add(orderItem);
			}
			await orderRepository.SaveChangesAsync();
			return orderId;
		}
    }
}
