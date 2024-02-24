using AutoMapper;
using Domain.Entities;

namespace Application.Models.Occurrences;

public class OccurrenceMappingProfile : Profile
{
    public OccurrenceMappingProfile()
    {
        CreateMap<Occurrence, OccurrenceModel>().ReverseMap();
    }
}