namespace Template.Application.SpecifiedMeasurements.Dtos
{
	public class SpecifiedMeasurementDto
	{
		public int Id { get; set; }
		public string Height { get; set; } = default!;
		public string Width { get; set; } = default!;
		public string Details { get; set; } = default!;
		public string? Measurement { get; set; }
		public string? ImagePath { get; set; }
	}
}
