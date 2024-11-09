using Microsoft.AspNetCore.Identity;
using Template.Domain.Entities.EngConsultation;
using Template.Domain.Entities.Orders;

namespace Template.Domain.Entities
{
	public class User : IdentityUser
	{
		public List<Order>? Orders { get; set; }
		public List<Consultation>? Consultations { get; set; }
	}
}
