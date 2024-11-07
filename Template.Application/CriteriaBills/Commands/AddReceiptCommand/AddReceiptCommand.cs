using MediatR;
using Microsoft.AspNetCore.Http;
using Template.Application.CriteriaBills.Dtos;

namespace Template.Application.CriteriaBills.Commands.AddReceiptCommand;

public class AddReceiptCommand : IRequest<CriteriaBillDto>
{
    public int Id { get; set; }
    public IFormFile Receipt { get; set; }
}