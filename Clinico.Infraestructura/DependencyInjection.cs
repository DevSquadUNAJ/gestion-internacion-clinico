using Clinico.Aplicacion.CasosDeUso;
using Clinico.Aplicacion.Interfaces.ICasosDeUso;
using Clinico.Aplicacion.Interfaces.IConsultas;
using Clinico.Aplicacion.Interfaces.IExternos;
using Clinico.Infraestructura.Consultas;
using Clinico.Infraestructura.Persistencia;
using Clinico.Infraestructura.Refit;
using Clinico.Infraestructura.Refit.Handlers;
using Clinico.Infraestructura.ServiciosExternos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;

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
                configuration.GetConnectionString("ClinicoDb"));
        });



        


        // ==========================================
        // CONFIGURACIÓN DE REFIT Y SERVICIOS EXTERNOS
        // ==========================================

        // Registramos el handler que inyectará el Token (usa ITokenUsuarioActual internamente)
        services.AddTransient<TokenDelegatingHandler>();

        var urlAdmision = configuration.GetValue<string>("UrlsExternas:Admision")
                          ?? throw new ArgumentNullException("La URL de Admision no esta configurada.");

        services.AddRefitClient<IAdmisionApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(urlAdmision))
            .AddHttpMessageHandler<TokenDelegatingHandler>();

        services.AddScoped<IAdmisionServicio, AdmisionServicio>();

        return services;
    }
}