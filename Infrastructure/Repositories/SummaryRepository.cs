using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SummaryRepository : ISummaryRepository
{
    private readonly AttendanceDbContext _context;
    
    public SummaryRepository(AttendanceDbContext context)
    {
        _context = context;
    }
    
    public IEnumerable<EmployeeSummary> GetSummaryForEmployee(int employeeId)
    {
        var result = _context.EmployeeSummaries
                .FromSqlInterpolated($"exec dbo.GetEmployeeSummary @employeeId = {employeeId}")
                .AsEnumerable();
        return result;
    }
}