using MediatR;

namespace Template.Application.Specifications.Commands.UpdateCommand
{
	public class UpdateSpecificationCommand : IRequest
	{
		public int SpecificationId { get; set; }
		public string Name { get; set; } = default!;
	}
}
