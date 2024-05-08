using Medicine.Dtos;
using Medicine.Models;
using Microsoft.EntityFrameworkCore;

namespace Medicine.Repository
{
    public class RepoDoctor : IDoctor
    {
        ApplicationDbContext context;

        public string Id { get; set; }

        public RepoDoctor(ApplicationDbContext _context)
        {

            Id = Guid.NewGuid().ToString();
            context = _context;
        }
        public List<SpecilizationDto> GetAll()
        {
            return context.Doctors
         .Include(d => d.User)
         .Include(d => d.Specialization)
         .Select(d => new SpecilizationDto
         {
             Id = d.Id,
             DoctorName = d.User.UserName,
             SpecilizationName = d.Specialization.Name
         })
         .ToList();
        }

        public Doctor GetById(int id)
        {
            return context.Doctors
                   .Include(d => d.User)
                   .Include(d => d.Qualifications)
                   .Include(d => d.Experiences)
                   .Include(d => d.TimeSlots)
                   .Include(d => d.Specialization)
                   .FirstOrDefault(d => d.Id == id);
        }
        public void Insert(Doctor obj)
        {
            context.Add(obj);
        }
        public void Update(Doctor obj)
        {
            context.Update(obj);
        }
        public void Delete(int id)
        {
            Doctor doctor = GetById(id);
            if (doctor != null)
            {
                context.Doctors.Remove(doctor);
            }
        }

        public  void Save()
        {
             context.SaveChanges();
        }
        public void UpdateQualifications(ICollection<Qualifications> existingQualifications, List<QualificationDto> updatedQualifications)
        {
            foreach (var qualification in updatedQualifications)
            {
                var existingQual = existingQualifications.FirstOrDefault(q => q.Id == qualification.Id);
                if (existingQual != null)
                {
                    existingQual.St_date = qualification.StartQualificationDate;
                    existingQual.En_date = qualification.EndQualificationsDate;
                    existingQual.Degree = qualification.Degree;
                    existingQual.University = qualification.University;
                }
                else
                {
                    existingQualifications.Add(new Qualifications
                    {
                        St_date = qualification.StartQualificationDate,
                        En_date = qualification.EndQualificationsDate,
                        Degree = qualification.Degree,
                        University = qualification.University
                    });
                }
            }

        }
        public void UpdateExperiences(ICollection<Experience> existingExperiences, List<ExperienceDto> updatedExperiences)
        {
            foreach (var experience in updatedExperiences)
            {
                var existingExper = existingExperiences.FirstOrDefault(q => q.Id == experience.Id);
                if (existingExper != null)
                {
                    existingExper.St_date = experience.StartExperienceDate;
                    existingExper.En_date = experience.EndExperienceDate;
                    existingExper.Position = experience.Position;
                    existingExper.Hospital = experience.Hospital;
                }
                else
                {
                    existingExperiences.Add(new Experience
                    {
                        St_date = experience.StartExperienceDate,
                        En_date = experience.EndExperienceDate,
                        Position = experience.Position,
                        Hospital = experience.Hospital
                    });
                }
            }

        }
        public void UpdateTimeSlot(ICollection<TimeSlots> existingTimeSlot, List<TimeSlotDto> updatedTimeSlot)
        {
            foreach (var timeSlot in updatedTimeSlot)
            {
                var existingTS = existingTimeSlot.FirstOrDefault(q => q.Id == timeSlot.Id);
                if (existingTS != null)
                {
                    existingTS.Day = timeSlot.DayTimeSlot;
                    existingTS.Form = timeSlot.Form;
                    existingTS.To = timeSlot.To;
                }
                else
                {
                    existingTimeSlot.Add(new TimeSlots
                    {
                        Day = timeSlot.DayTimeSlot,
                        Form = timeSlot.Form,
                        To = timeSlot.To

                    });
                }
            }

        }
        //public void UpdateSpecialization(ICollection<Specialization> existingSpecialization, Specialization updatedSpecialization)
        //{
        //        var existingSP = existingSpecialization.FirstOrDefault(q => q.Id == updatedSpecialization.Id);
        //        if (existingSP != null)
        //        {
        //        // Update existing qualification
        //        existingSP.Name = updatedSpecialization.Name;
                  
        //        }
        //        else
        //        {
        //            existingSpecialization.Add(new Specialization
        //            {
        //                Name = updatedSpecialization.Name,
                       

        //            });
        //        }
        //    }

        }
    
}
