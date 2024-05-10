using Medicine.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medicine.Dtos.Patient
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string BloodType { get; set; }
        public string UsreName { get; set; }
        public string ImageUrl { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { set; get; }


    }

}
