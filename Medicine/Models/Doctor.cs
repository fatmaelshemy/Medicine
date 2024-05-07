using System.ComponentModel.DataAnnotations.Schema;

namespace Medicine.Models
{
	public class Doctor
	{
		public int Id { get; set; }
		public string Bio { get; set; }
		public string About { get; set; }
		public string Location { get; set; } //مكان تواجد الدكتور 
		public List<Experience>? Experiences { get; set; }
		public List<Qualifications>? Qualifications { get; set; }
		public List<TimeSlots>? TimeSlots { get; set; }

		public float TicketPrice { get; set; }

        [ForeignKey("Specialization")]
        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; }

        [ForeignKey("User")]
		public string userId { get; set; }
		public ApplicationUser? User { get; set; }


	}
}
