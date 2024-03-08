using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Infrastructure;

public static class ControllerMapperExtension
{
    public static IEndpointRouteBuilder MapCustomControllers(this IEndpointRouteBuilder builder)
    {
        builder.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        builder.MapControllerRoute(
            name: "Occurrences",
            pattern: "{controller=Occurrence}/{action=Occurrences}/{id?}");
        builder.MapControllerRoute(
            name: "Attendances",
            pattern: "{controller=Attendance}/{action=Attendances}/{id?}");
        return builder;
    }
}