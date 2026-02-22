using MeuServico.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeuServico.Infrastructure;

using BackEndApi.Service;
using MeuServico.Application.Dtos;
using MeuServico.Application.Interfaces;
using MeuServico.Application.Services;
using MeuServico.Core.Entities;
using MeuServico.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
     this IServiceCollection services,
     IConfiguration configuration)
    {
        // DbContext
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(
                configuration.GetConnectionString("padrao")));

        // =============================
        // ✅ REPOSITORY
        // =============================
        services.AddScoped(typeof(IBaseRepository<>),
                           typeof(BaseRepository<>));

        // =============================
        // ✅ BASE SERVICE (GENÉRICO)
        // =============================
        services.AddScoped<IBaseService<CarroDto>, CarroService>();
        services.AddScoped<IBaseService<LocacaoDto>, LocacaoService>();
        services.AddScoped<IBaseService<ManutencaoDto>, ManutencaoService>();
        services.AddScoped<IBaseService<DespesaGeralDto>, DespesaGeralService>();
        services.AddScoped<IBaseService<EmpresaDto>, EmpresaService>();
        services.AddScoped<IBaseService<ClienteDto>, ClienteService>();

        // =============================
        // ✅ SERVICES ESPECÍFICOS
        // =============================
        services.AddScoped<CarroService>();
        services.AddScoped<LocacaoService>();
        services.AddScoped<ManutencaoService>();
        services.AddScoped<DespesaGeralService>();
        services.AddScoped<EmpresaService>();
        services.AddScoped<ClienteService>();

        return services;
    }
}