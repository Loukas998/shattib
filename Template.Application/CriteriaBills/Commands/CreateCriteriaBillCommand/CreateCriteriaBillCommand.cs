using MediatR;
using Microsoft.AspNetCore.Http;

namespace Template.Application.CriteriaBills.Commands.CreateCriteriaBillCommand;

public class CreateCriteriaBillCommand : IRequest<int>
{
    public IFormFile Image { get; set; } = default!;
    public int CriteriaId { get; set; } = default!;
}