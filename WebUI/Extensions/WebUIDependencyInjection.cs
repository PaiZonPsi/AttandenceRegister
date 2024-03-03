using Infrastructure.Extensions;
using Newtonsoft.Json.Serialization;

namespace AttendanceRegister.Extensions;

public static class WebUiDependencyInjection
{
    public static IServiceCollection AddWebUi(this IServiceCollection services)
    {
        services.AddMvc().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ContractResolver = new DefaultContractResolver();
        });

        services.AddSwaggerGen();
        services.AddScoped<Seed>();
        services.AddKendo();
        services.AddEndpointsApiExplorer();
        return services;
    }
}