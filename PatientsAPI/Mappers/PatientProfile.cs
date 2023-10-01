using AutoMapper;
using PatientsAPI.Database.Models;
using PatientsAPI.DTOs;

namespace PatientsAPI.Mappers
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<PatientRequest, Patient>()
                .ReverseMap();
            CreateMap<Patient, PatientResponse>()
                .ReverseMap();
        }
    }
}
