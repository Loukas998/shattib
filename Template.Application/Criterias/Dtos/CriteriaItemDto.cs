using Microsoft.AspNetCore.Http;

namespace Template.Application.Criterias.Dtos;

public class CriteriaItemDto
{
    public int CategoryId { get; set; } = default!;
    public string ProductName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Amount { get; set; } = default!;
    public string MeasurementUnit { get; set; } = default!;
    public IFormFile Image { get; set; } = default!;
}