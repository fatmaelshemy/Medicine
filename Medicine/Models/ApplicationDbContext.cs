using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Medicine.Models
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public DbSet<Doctor> Doctors { get; set; }
		public DbSet<Patient> Patients { get; set; }
		public DbSet<Contact> Contacts { get; set; }
		public DbSet<Qualifications> Qualifications { get; set; }
		public DbSet<Experience> Experience { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<Specialization> Specializations { get; set; }
		public DbSet<TimeSlots> TimeSlots { get; set; }
		public DbSet<Booking> Bookings { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}
	}
}
