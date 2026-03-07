using MeuServico.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

public class AppDbContextFactory
    : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var apiPath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "../MeuServico.API");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(apiPath)
            .AddJsonFile("appsettings.json")
            .Build();

        var dbPath = Path.Combine(
            apiPath,
            "Data",
            "MeuServico.db");

        var optionsBuilder =
            new DbContextOptionsBuilder<AppDbContext>();

        optionsBuilder.UseSqlite(
            $"Data Source={dbPath}");

        return new AppDbContext(optionsBuilder.Options);
    }
}