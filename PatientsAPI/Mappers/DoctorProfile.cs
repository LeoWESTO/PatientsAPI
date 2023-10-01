using AutoMapper;
using PatientsAPI.Database.Models;
using PatientsAPI.DTOs;

namespace PatientsAPI.Mappers
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<DoctorRequest, Doctor>()
                .ReverseMap();
            CreateMap<Doctor, DoctorResponse>()
                .ReverseMap();
        }
    }
}
