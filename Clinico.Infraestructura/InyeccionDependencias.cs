using Clinico.Aplicacion.Interfaces.IComandos;
using Clinico.Aplicacion.Interfaces.IConsultas;
using Clinico.Aplicacion.Interfaces.IExternos;
using Clinico.Infraestructura.Comandos;
using Clinico.Infraestructura.Consultas;
using Clinico.Infraestructura.IA.Configuracion;
using Clinico.Infraestructura.IA.Gemini;
using Clinico.Infraestructura.IA.Groq;
using Clinico.Infraestructura.IA.Mock;
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
        services.AddScoped<IDiagnosticoConsulta, DiagnosticoConsulta>();
        services.AddScoped<IMedicamentoConsulta, MedicamentoConsulta>();
        services.AddScoped<IUnidadMedidaConsulta, UnidadMedidaConsulta>();
        services.AddScoped<IAuditoriaIAConsulta, AuditoriaIAConsulta>();

        // ==========================================
        // COMANDOS
        // ==========================================
        services.AddScoped<IDiagnosticoComando, DiagnosticoComando>();
        services.AddScoped<IEvolucionClinicaComando, EvolucionClinicaComando>();
        services.AddScoped<ITratamientoComando, TratamientoComando>();
        services.AddScoped<ITratamientoDosisComando, TratamientoDosisComando>();
        services.AddScoped<IAuditoriaIAComando, AuditoriaIAComando>();
        services.AddScoped<IAuditoriaComando, AuditoriaComando>();

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

        // ==========================================
        // IA - VALIDADOR CLÍNICO
        // ==========================================
        services.Configure<OpcionesIA>(configuration.GetSection(OpcionesIA.SeccionConfiguracion));

        var proveedorIA = configuration.GetValue<string>($"{OpcionesIA.SeccionConfiguracion}:Proveedor") ?? "Mock";

        Console.WriteLine($"[IA] Proveedor seleccionado: {proveedorIA}");

        if (string.Equals(proveedorIA, "Gemini", StringComparison.OrdinalIgnoreCase))
        {
            var apiKey = configuration.GetValue<string>($"{OpcionesIA.SeccionConfiguracion}:Gemini:ApiKey");
            if (string.IsNullOrWhiteSpace(apiKey) || apiKey == "TU_API_KEY_AQUI")
                throw new InvalidOperationException("Proveedor 'Gemini' seleccionado pero falta la API key.");

            Console.WriteLine($"[IA] Modelo Gemini: {configuration.GetValue<string>($"{OpcionesIA.SeccionConfiguracion}:Gemini:Modelo")}");
            services.AddHttpClient<IValidadorClinicoIA, ValidadorClinicoGeminiServicio>();
        }
        else if (string.Equals(proveedorIA, "Groq", StringComparison.OrdinalIgnoreCase))
        {
            var apiKey = configuration.GetValue<string>($"{OpcionesIA.SeccionConfiguracion}:Groq:ApiKey");
            if (string.IsNullOrWhiteSpace(apiKey) || apiKey == "TU_API_KEY_AQUI")
                throw new InvalidOperationException("Proveedor 'Groq' seleccionado pero falta la API key.");

            Console.WriteLine($"[IA] Modelo Groq: {configuration.GetValue<string>($"{OpcionesIA.SeccionConfiguracion}:Groq:Modelo")}");
            Console.WriteLine($"[IA] API Key Groq cargada: {apiKey[..6]}...{apiKey[^4..]} (longitud: {apiKey.Length})");
            services.AddHttpClient<IValidadorClinicoIA, ValidadorClinicoGroqServicio>();
        }
        else
        {
            Console.WriteLine("[IA] Usando validador Mock determinístico.");
            services.AddScoped<IValidadorClinicoIA, ValidadorClinicoMockServicio>();
        }

        return services;
    }
}