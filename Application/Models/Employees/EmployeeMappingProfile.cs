using AutoMapper;
using Domain.Entities;

namespace Application.Models.Employees;

public class EmployeeMappingProfile : Profile
{
    public EmployeeMappingProfile()
    {
        CreateMap<Employee, EmployeeModel>().ReverseMap();
    }
}