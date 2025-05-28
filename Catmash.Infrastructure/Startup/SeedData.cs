using Catmash.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;


namespace Catmash.Infrastructure.Startup
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("SeedData");

            if (await context.Cats.AnyAsync())
            {
                logger.LogInformation("Cats already exist. Skipping seeding.");
                return;
            }

            var url = config["CatData:SeedUrl"];
            if (string.IsNullOrEmpty(url))
            {
                logger.LogWarning("No seed URL found in configuration.");
                return;
            }

            try
            {
                using var http = new HttpClient();
                var response = await http.GetFromJsonAsync<CatApiResponse>(url);

                if (response?.Images != null)
                {
                    var cats = response.Images.Select(img => new Cat
                    {
                        Id = img.Id,
                        ImageUrl = img.Url,
                        Score = 1000,
                        Wins = 0,
                        Losses = 0
                    });

                    await context.Cats.AddRangeAsync(cats);
                    await context.SaveChangesAsync();

                    logger.LogInformation("Seeded {Count} cats successfully.", cats.Count());
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error during cat seeding.");
            }
        }

    }
}
