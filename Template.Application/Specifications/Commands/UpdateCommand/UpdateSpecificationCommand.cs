using MediatR;

namespace Template.Application.Specifications.Commands.UpdateCommand
{
	public class UpdateSpecificationCommand(int specificationId) : IRequest
	{
		public int SpecificationId { get; } = specificationId;
		public string Name { get; set; } = default!;
	}
}
