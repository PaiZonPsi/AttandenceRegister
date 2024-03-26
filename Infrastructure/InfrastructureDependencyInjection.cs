using Application.Interfaces.Repository;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AttendanceDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlServerLocalConnectionString")));
        services.AddTransient<IEmployeeRepository, EmployeeRepository>();
        services.AddTransient<IAttendanceRepository, AttendanceRepository>();
        services.AddTransient<IOccurrenceRepository, OccurrenceRepository>();
        services.AddTransient<ISummaryRepository, SummaryRepository>();
        return services;
    }
}