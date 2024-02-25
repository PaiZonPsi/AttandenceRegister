using Application.Interfaces.Repository;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<AttendanceDbContext>();
        services.AddTransient<IEmployeeRepository, EmployeeRepository>();
        services.AddTransient<IAttendanceRepository, AttendanceRepository>();
        services.AddTransient<IOccurrenceRepository, OccurrenceRepository>();
        return services;
    }
}