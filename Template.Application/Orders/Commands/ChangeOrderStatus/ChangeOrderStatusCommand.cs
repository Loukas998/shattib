using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Template.Application.Orders.Commands.ChangeOrderStatus
{
	public class ChangeOrderStatusCommand : IRequest
	{
		public int OrderId { get; set; }
		public string NewStatus { get; set; } = default!;
		[DataType(DataType.Date)]
		public DateTime? DateOfArrival { get; set; }
	}
}
