using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Clinico.Infraestructura.Persistencia;

namespace Clinico.Infraestructura;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructura(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // agregar el contexto de la base de datos
        services.AddDbContext<ContextoBaseDeDatos>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("ClinicaDb"));
        });

        return services;
    }
}