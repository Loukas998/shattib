using MediatR;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Template.Application.Criterias.Dtos;

namespace Template.Application.Criterias.Commands.CreateCriteriaCommand;

public class CreateCriteriaCommand : IRequest<int>
{
    public string Title { get; set; } = default!;
	[DataType(DataType.Date)]
	public DateTime DateOfCreation { get; set; } = default!;
	public List<CreateCriteriaItemDto> CriteriaItems { get; set; } = default!;
}