using Application.ExceptionHandlers;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(ApplicationDependencyInjection).Assembly;
        services.AddAutoMapper(assembly);
        services.AddValidatorsFromAssembly(assembly);
        services.AddExceptionHandler<BadRequestExceptionHandler>();
        services.AddExceptionHandler<InternalServerExceptionHandler>();
        return services;
    }
}