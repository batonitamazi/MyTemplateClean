using Microsoft.AspNetCore.Builder;

namespace MyTemplateClean.Infrastructure.Data.Extensions;

public static class DatabaseExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        context.Database.MigrateAsync().GetAwaiter().GetResult();

        await SeedAsync(context);
    }
    private static async Task SeedAsync(ApplicationDbContext context)
    {
        await Task.Yield();
        //here you can implement some Seeding logic
    }

}