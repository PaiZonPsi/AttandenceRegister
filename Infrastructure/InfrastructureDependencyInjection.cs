using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AttendanceDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlServerLocalConnectionString")));
        RegisterRepositories(services);
        RegisterServices(services);

        return services;
    }

    private static void RegisterServices(IServiceCollection services)
    {
        services.AddTransient<IEmployeeService, EmployeeService>();
        services.AddTransient<IOccurrenceService, OccurrenceService>();
        services.AddTransient<IAttendanceRegisterService, AttendanceRegisterService>();
    }

    private static void RegisterRepositories(IServiceCollection services)
    {
        services.AddTransient<IEmployeeRepository, EmployeeRepository>();
        services.AddTransient<IAttendanceRepository, AttendanceRepository>();
        services.AddTransient<IOccurrenceRepository, OccurrenceRepository>();
        services.AddTransient<ISummaryRepository, SummaryRepository>();
    }
}