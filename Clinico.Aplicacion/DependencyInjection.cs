using Clinico.Aplicacion.CasosDeUso;
using Clinico.Aplicacion.Interfaces.ICasosDeUso;
using Microsoft.Extensions.DependencyInjection;

namespace Clinico.Aplicacion;

public static class DependencyInjection
{
    public static IServiceCollection AddAplicacion(
        this IServiceCollection services)
    {
        services.AddScoped<IGetNursingDashboardUseCase, GetNursingDashboardUseCase>();

        return services;
    }
}