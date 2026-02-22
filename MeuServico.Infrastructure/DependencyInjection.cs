using MeuServico.Application.Interfaces;
using MeuServico.Infrastructure.Persistence;
using MeuServico.Infrastructure.Repositories;
using MeuServico.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeuServico.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opt =>
            opt.UseSqlite(
                configuration.GetConnectionString("Padrao")));

        services.AddScoped(typeof(IBaseRepository<>),
                           typeof(BaseRepository<>));

        return services;
    }
}