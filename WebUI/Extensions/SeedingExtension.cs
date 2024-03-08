using Infrastructure.Extensions;

namespace AttendanceRegister.Extensions;

public static class SeedingExtension
{
    public static void SeedData(this IHost app)
    {
        using var scope = app.Services.CreateScope();
        var seeder = scope.ServiceProvider.GetRequiredService<Seed>();
        seeder.SeedEmployees();
        seeder.SeedOccurrences();
    }
}