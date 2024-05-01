using System.ComponentModel.DataAnnotations.Schema;

namespace Medicine.Models
{
	public class Patient
	{
		public int Id { get; set; }
		public string BloodType { get; set; }

		[ForeignKey("User")]
		public string userId { get; set; }
		public ApplicationUser? User { get; set; }
	}
}
