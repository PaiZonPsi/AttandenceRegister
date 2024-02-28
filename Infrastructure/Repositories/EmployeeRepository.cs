using Application.Interfaces.Repository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
{
    private readonly AttendanceDbContext _context;
    public EmployeeRepository(AttendanceDbContext context) : base(context)
    {
        _context = context;
    }
}