using Clinico.Aplicacion.DTOs;
using Clinico.Aplicacion.Interfaces.IConsultas;
using Clinico.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Clinico.Infraestructura.Consultas
{
    public class ObtenerEnfermeraPanelDeControlConsulta : IObtenerEnfermeraPanelDeControlConsulta
    {
        private readonly ContextoBaseDeDatos _contexto;

        public ObtenerEnfermeraPanelDeControlConsulta(ContextoBaseDeDatos contexto)
        {
            _contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
        }

        public async Task<IReadOnlyCollection<EnfermeraPanelDeControlDto>> ObtenerSectorPendienteAsync(Guid sectorId, CancellationToken cancellationToken)
        {
            // Traer todas las dosis (ajustar filtro si hay propiedad directa para sector/enfermera en la entidad)
            var dosis = await _contexto.TratamientosDosis
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            // Filtrar por SectorId si existe la propiedad; si no existe, devolvemos vacío
            var filtradas = dosis.Where(d =>
            {
                var prop = d.GetType().GetProperty("SectorId", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (prop == null) return false;
                var val = prop.GetValue(d);
                if (val is Guid g) return g == sectorId;
                if (val is string s && Guid.TryParse(s, out g)) return g == sectorId;
                return false;
            });

            var resultado = filtradas
                .Select(d =>
                {
                    Guid treatmentDoseId = ObtenerGuid(d, "Id") != Guid.Empty
                        ? ObtenerGuid(d, "Id")
                        : ObtenerGuid(d, "TratamientoDosisId");

                    string patient = ObtenerString(d, "Paciente")
                        ?? ObtenerString(d, "Patient")
                        ?? string.Empty;

                    string medication = ObtenerString(d, "Medicamento")
                        ?? ObtenerString(d, "Medication")
                        ?? string.Empty;

                    DateTime scheduledTime = ObtenerDateTime(d, "HoraProgramada")
                        ?? ObtenerDateTime(d, "ScheduledTime")
                        ?? DateTime.MinValue;

                    return new EnfermeraPanelDeControlDto(
                        treatmentDoseId,
                        patient,
                        medication,
                        scheduledTime
                    );
                })
                .ToList();

            return resultado;
        }

        private static Guid ObtenerGuid(object obj, string propName)
        {
            var prop = obj.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (prop == null) return Guid.Empty;
            var val = prop.GetValue(obj);
            if (val is Guid g) return g;
            if (val is string s && Guid.TryParse(s, out g)) return g;
            return Guid.Empty;
        }

        private static string? ObtenerString(object obj, string propName)
        {
            var prop = obj.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (prop == null) return null;
            var val = prop.GetValue(obj);
            return val?.ToString();
        }

        private static DateTime? ObtenerDateTime(object obj, string propName)
        {
            var prop = obj.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (prop == null) return null;
            var val = prop.GetValue(obj);
            if (val is DateTime dt) return dt;
            if (val is string s && DateTime.TryParse(s, out dt)) return dt;
            return null;
        }
    }
}