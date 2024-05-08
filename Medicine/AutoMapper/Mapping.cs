using AutoMapper;
using Medicine.Dtos.Patient;
using Medicine.Models;

namespace Medicine.AutoMapper
{
    public class Mapping:Profile
    {

        public Mapping()
        {
            CreateMap<Patient,PatientDto>();
            CreateMap<AddPatientDto, Patient>();
           CreateMap<Patient, AddPatientDto>();
            CreateMap<PatientDto, Patient>();
            CreateMap<ApplicationUserDto, ApplicationUser>();
        }
    }
}
