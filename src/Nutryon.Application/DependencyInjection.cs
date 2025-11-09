
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Nutryon.Application;
using Nutryon.Application.Users;
using Nutryon.Application.Refeicoes;

namespace Nutryon.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // AutoMapper
        services.AddAutoMapper(typeof(MappingProfile));
        // UseCases
        services.AddScoped<CreateUsuarioUseCase>();
        services.AddScoped<UpdateUsuarioUseCase>();
        services.AddScoped<DeleteUsuarioUseCase>();
        services.AddScoped<ListUsuariosUseCase>();
        services.AddScoped<CreateRefeicaoComItensUseCase>();
        services.AddScoped<ListarRefeicoesPorDiaUseCase>();
        return services;
    }
}
