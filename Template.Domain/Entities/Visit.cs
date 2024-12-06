using System.ComponentModel.DataAnnotations;

namespace Template.Domain.Entities
{
	public class Visit
	{
		public int Id { get; set; }
		public int NumberOfVisitors { get; set; }
		public string UserIp { get; set; } = default!;
		public string UserRemotePort { get; set; } = default!;
		[DataType(DataType.Date)]
		public DateTime DateOfVisits { get; set; } = DateTime.Now;
	}
}
