using MediatR;
using Template.Application.CriteriaBills.Dtos;

namespace Template.Application.CriteriaBills.Queries.GetCriteriaBillByIdQuery;

public class GetCriteriaBillByIdQuery(int billId) : IRequest<CriteriaBillDto>
{
    public int Id { get; set; } = billId;
}