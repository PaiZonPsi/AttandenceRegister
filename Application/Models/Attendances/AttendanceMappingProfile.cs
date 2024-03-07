using AutoMapper;
using Domain.Entities;

namespace Application.Models.Attendances;

public class AttendanceMappingProfile : Profile
{
    public AttendanceMappingProfile()
    {
        CreateMap<Attendance, AttendanceModel>()
            .ForMember(a => a.OccurrenceStartDate, opt => opt.MapFrom(a => a.OccurrenceStartDate.ToDateTime(new TimeOnly())))
            .ForMember(a => a.OccurrenceEndDate, opt => opt.MapFrom(a => a.OccurrenceEndDate.ToDateTime(new TimeOnly())));
    }
}