using Clinico.Aplicacion.Interfaces.IComandos;
using Clinico.Aplicacion.Interfaces.IConsultas;
using Clinico.Aplicacion.Interfaces.IExternos;
using Clinico.Infraestructura.Comandos;
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

public static class InyeccionDependencias
{
    public static IServiceCollection AddInfraestructura(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // ==========================================
        // BASE DE DATOS
        // ==========================================
        services.AddDbContext<ContextoBaseDeDatos>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("ClinicoDb"));
        });

        // ==========================================
        // CONSULTAS
        // ==========================================
        services.AddScoped<IHistoriaClinicaConsulta, HistoriaClinicaConsulta>();
        services.AddScoped<IMedicoConsulta, MedicoConsulta>();
        services.AddScoped<ICatalogoCie10Consulta, CatalogoCie10Consulta>();
        services.AddScoped<ITratamientoConsulta, TratamientoConsulta>();
        services.AddScoped<IFrecuenciaAdministracionConsulta, FrecuenciaAdministracionConsulta>();
        services.AddScoped<ITratamientoDosisConsulta, TratamientoDosisConsulta>();
        services.AddScoped<IObtenerEnfermeraConsulta, ObtenerEnfermeraConsulta>();
        services.AddScoped<IObtenerTratamientoDosisConsulta, ObtenerTratamientoDosisConsulta>();
        services.AddScoped<IHistorialAuditoriaConsulta, HistorialAuditoriaConsulta>();

        // ==========================================
        // COMANDOS
        // ==========================================
        services.AddScoped<IDiagnosticoComando, DiagnosticoComando>();
        services.AddScoped<IEvolucionClinicaComando, EvolucionClinicaComando>();
        services.AddScoped<ITratamientoComando, TratamientoComando>();
        services.AddScoped<ITratamientoDosisComando, TratamientoDosisComando>();

        // ==========================================
        // REFIT Y SERVICIOS EXTERNOS
        // ==========================================
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