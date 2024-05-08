using Medicine.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medicine.Dtos.Patient
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string BloodType { get; set; }
        public string userId { get; set; }

        public ApplicationUserDto? User { get; set; }
    }

}
