namespace Template.Domain.Entities.Orders
{
	public class SpecifiedMeasurement
	{
		public int Id { get; set; }
		public string UserId { get; set; } = default!;
		public string Height { get; set; } = default!;
		public string Width { get; set; } = default!;
		public string Details { get; set; } = default!;
		public string? Measurement { get; set; }
		public string? ImagePath { get; set; }
		public User User { get; set; }
	}
}
