namespace Template.Domain.Entities
{
	public class ContactUs
	{
		public int Id { get; set; }
		public string Name { get; set; } = default!;
		public string Email { get; set; } = default!;
		public string PhoneNumber { get; set; } = default!;
		public string Message { get; set; } = default!;
	}
}
