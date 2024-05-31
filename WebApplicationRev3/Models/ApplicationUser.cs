using Microsoft.AspNetCore.Identity;

namespace WebApplicationRev3.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string FirstName { get; set; } = "";
		public string LastName { get; set; } = "";
		public DateTime CreationDate { get; set; }
	}
}
