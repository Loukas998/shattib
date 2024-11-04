namespace Template.Application.Criterias.Dtos;

public class GetCriteriaItemDto
{
    public int CategoryId { get; set; } = default!;
    public string CategoryName { get; set; } = default!;
    public string ProductName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Amount { get; set; } = default!;
    public string MeasurementUnit { get; set; } = default!;
    public string Image { get; set; } = default!;
}