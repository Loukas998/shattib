using MediatR;

namespace Template.Application.Specifications.Commands.CreateCommand
{
    public class CreateSpecificationCommand : IRequest<int>
    {
        public string Name { get; set; } = default!;
    }
}
