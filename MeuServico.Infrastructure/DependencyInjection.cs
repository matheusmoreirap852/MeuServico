using MeuServico.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeuServico.Infrastructure;

using BackEndApi.Service;
using MeuServico.Application.Dtos;
using MeuServico.Application.Interfaces;
using MeuServico.Application.Services;
using MeuServico.Infrastructure.Persistence;
using MeuServico.Infrastructure.Repositories;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // =============================
        // DbContext
        // =============================
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(
                configuration.GetConnectionString("padrao")));

        // =============================
        // REPOSITORIES
        // =============================
        services.AddScoped(typeof(IBaseRepository<>),
                           typeof(BaseRepository<>));

        services.AddScoped<ICarroRepository, CarroRepository>();

        // =============================
        // SERVICES
        // =============================
      
        services.AddScoped<IBaseService<CarroDto>, CarroService>();
        services.AddScoped<IBaseService<LocacaoDto>, LocacaoService>();
        services.AddScoped<IBaseService<ManutencaoDto>, ManutencaoService>();
        services.AddScoped<IBaseService<DespesaGeralDto>, DespesaGeralService>();
        services.AddScoped<IBaseService<EmpresaDto>, EmpresaService>();
        services.AddScoped<IBaseService<ClienteDto>, ClienteService>();
        services.AddScoped<ICarroService, CarroService>();

        return services;
    }
}