using BeaconSync.Application.Services;
using BeaconSync.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BeaconSync.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Registro de todos os Services da Aplicação
        services.AddScoped<IPessoaVulneravelService, PessoaVulneravelService>();
        services.AddScoped<IResponsavelService, ResponsavelService>();
        services.AddScoped<IEnderecoService, EnderecoService>();
        services.AddScoped<IVoluntarioService, VoluntarioService>();
        services.AddScoped<IRespostaAlertaService, RespostaAlertaService>();

        return services;
    }
}