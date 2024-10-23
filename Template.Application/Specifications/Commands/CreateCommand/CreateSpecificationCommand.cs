using MediatR;

namespace Template.Application.Specifications.Commands.CreateCommand
{
    public class CreateSpecificationCommand : IRequest
    {
        public string Name { get; set; } = default!;
    }
}
