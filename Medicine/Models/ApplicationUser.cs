using Microsoft.AspNetCore.Identity;

namespace Medicine.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string? Gender { get; set; }
		public string? ImageUrl { get; set; }

	}
}
