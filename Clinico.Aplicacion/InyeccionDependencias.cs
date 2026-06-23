using Clinico.Aplicacion.CasosDeUso;
using Clinico.Aplicacion.Interfaces.ICasosDeUso;
using Clinico.Aplicacion.Interfaces.IMapeadores;
using Clinico.Aplicacion.Mapeadores;
using Microsoft.Extensions.DependencyInjection;

namespace Clinico.Aplicacion;

public static class InyeccionDependencias
{
    public static IServiceCollection AddAplicacion(this IServiceCollection services)
    {
        // ==========================================
        // CASOS DE USO
        // ==========================================
        services.AddScoped<IObtenerHistoriaClinicaCasoDeUso, ObtenerHistoriaClinicaCasoDeUso>();
        services.AddScoped<IRegistrarDiagnosticoCasoDeUso, RegistrarDiagnosticoCasoDeUso>();
        services.AddScoped<IRegistrarEvolucionClinicaCasoDeUso, RegistrarEvolucionClinicaCasoDeUso>();
        services.AddScoped<IModificarTratamientoCasoDeUso, ModificarTratamientoCasoDeUso>();
        services.AddScoped<IObtenerSeguimientoTratamientoCasoDeUso, ObtenerSeguimientoTratamientoCasoDeUso>();
        services.AddScoped<IObtenerHistorialAuditoriaCasoDeUso, ObtenerHistorialAuditoriaCasoDeUso>();
        services.AddScoped<IObtenerEnfermeraPanelDeControlCasoDeUso, ObtenerPanelDeControlEnfermeraCasoDeUso>();
        services.AddScoped<IRegistrarAdministracionMedicacionCasoDeUso, RegistrarAdministracionMedicacionCasoDeUso>();
        services.AddScoped<IRegistrarOmisionMedicacionCasoDeUso, RegistrarOmisionMedicacionCasoDeUso>();

        // ==========================================
        // MAPEADORES
        // ==========================================
        services.AddScoped<IHistoriaClinicaMapper, HistoriaClinicaMapper>();
        services.AddSingleton<IRegistrarDiagnosticoMapeador, RegistrarDiagnosticoMapeador>();
        services.AddScoped<IHistorialAuditoriaMapper, HistorialAuditoriaMapper>();

        return services;
    }
}