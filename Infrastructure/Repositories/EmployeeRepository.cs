using Application.Interfaces.Repository;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(AttendanceDbContext context) : base(context)
    {
    }
}