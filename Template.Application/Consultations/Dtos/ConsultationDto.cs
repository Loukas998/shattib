using System.ComponentModel.DataAnnotations;

namespace Template.Application.Consultations.Dtos
{
	public class ConsultationDto
	{
		public int Id { get; set; }
		public string ClientName { get; set; } = default!;
		public string Email { get; set; } = default!;
		public string PhoneNumber { get; set; } = default!;
		public string ConsultationTopic { get; set; } = default!;
		public string EngineerSpecification { get; set; } = default!;
		public string ProjectCategory { get; set; } = default!;
		public string Details { get; set; } = default!;
		[DataType(DataType.Date)]
		public DateTime DateOfRequest { get; set; } = default!;
	}
}
