using AutoMapper;
using Domain.Entities;

namespace Application.Models.Attendances;

public class AttendanceMappingProfile : Profile
{
    public AttendanceMappingProfile()
    {
        CreateMap<Attendance, AttendanceModel>().ReverseMap();
    }
}