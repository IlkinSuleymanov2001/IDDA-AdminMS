using Application.Repositories.Context;
using Infastructure.Repositories.Context;
using Microsoft.EntityFrameworkCore;

namespace AdminApi.Commons.Extensions
{
    public static class AutoMigrationExtension
    {

        public static async Task<WebApplication> ApplyMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AdminContext>();
            await db.Database.MigrateAsync();
            return app;
        }
    }
}
