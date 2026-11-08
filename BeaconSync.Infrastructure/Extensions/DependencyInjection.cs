using BeaconSync.Infrastructure.Data;
using BeaconSync.Infrastructure.Repositories;
using BeaconSync.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BeaconSync.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseOracle(configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

        // Registro de todos os Repositórios
        services.AddScoped<IPessoaVulneravelRepository, PessoaVulneravelRepository>();
        services.AddScoped<IResponsavelRepository, ResponsavelRepository>();
        services.AddScoped<IEnderecoRepository, EnderecoRepository>();
        services.AddScoped<IVoluntarioRepository, VoluntarioRepository>();
        services.AddScoped<IRespostaAlertaRepository, RespostaAlertaRepository>();

        return services;
    }
}