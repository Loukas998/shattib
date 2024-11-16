using System.ComponentModel.DataAnnotations;

namespace Template.Domain.Entities
{
	public class OneTimePassword
	{
		public int Id { get; set; }
		public string PhoneNumber { get; set; } = default!;
		public string Code { get; set; } = default!;
		public bool IsActive { get; set; } = false;

		[DataType(DataType.Time)]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
		public DateTime? CreatedAt { get; set; }

		[DataType(DataType.Time)]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
		public DateTime? ActiveUntil { get; set; }
	}
}
