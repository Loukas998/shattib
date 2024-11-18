using Microsoft.AspNetCore.Identity;
using Template.Domain.Entities.Criterias;
using Template.Domain.Entities.EngConsultation;
using Template.Domain.Entities.Orders;

namespace Template.Domain.Entities
{
	public class User : IdentityUser
	{
		public string DisplayName { get; set; } = default!;
		public bool IsActive { get; set; } = true;
		public List<Order>? Orders { get; set; }
		public List<Consultation>? Consultations { get; set; }
		public List<Criteria>? Criterias { get; set; }
		public List<SpecifiedMeasurement>? SpecifiedMeasurements { get; set; }
	}
}
