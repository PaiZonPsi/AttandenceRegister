using AttendanceRegister.Factories;
using Infrastructure.Extensions;
using Infrastructure.Services;
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
        services.AddSingleton<IContentResultFactory, ContentResultFactory>();
        services.AddScoped<Seed>();
        services.AddKendo();
        services.AddEndpointsApiExplorer();
        return services;
    }
}