using Domain.Entities;
using Infrastructure.Extensions;
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.SeedEmployees();
        modelBuilder.SeedOccurrences();
    }
}