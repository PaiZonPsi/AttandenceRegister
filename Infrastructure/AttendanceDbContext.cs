using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AttendanceDbContext : DbContext
{
    public DbSet<Attendance> Attendances => Set<Attendance>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Occurrence> Occurrences => Set<Occurrence>();

    public AttendanceDbContext()
    {
        
    }
    
    public AttendanceDbContext(DbContextOptions<AttendanceDbContext> options) : base(options)
    {
        
    }
}