using Clinico.Aplicacion.DTOs.Respuestas.IA;
using Clinico.Aplicacion.DTOs.Solicitudes.IA;
using Clinico.Aplicacion.Interfaces.IExternos;
using Clinico.Dominio.Constantes;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Clinico.Infraestructura.IA.Mock
{
    public class ValidadorClinicoMockServicio : IValidadorClinicoIA
    {
        public Task<AnalisisIAResultado> AnalizarAsync(
            ContextoClinicoIADto contexto,
            CancellationToken cancellationToken)
        {
            var alertas = new List<AlertaIADto>();

            // Regla 1: alergia declarada coincide con el medicamento propuesto
            if (!string.IsNullOrWhiteSpace(contexto.Paciente.Alergias))
            {
                var alergiasLower = contexto.Paciente.Alergias.ToLowerInvariant();
                var drogaLower = contexto.TratamientoPropuesto.DrogaGenerica.ToLowerInvariant();
                var medicamentoLower = contexto.TratamientoPropuesto.Medicamento.ToLowerInvariant();

                if (alergiasLower.Contains(drogaLower) || alergiasLower.Contains(medicamentoLower))
                {
                    alertas.Add(new AlertaIADto
                    {
                        Tipo = TipoAlertaIA.Alergia,
                        Severidad = SeveridadAlertaIA.Critica,
                        Descripcion = $"El paciente declara alergia compatible con {contexto.TratamientoPropuesto.DrogaGenerica}."
                    });
                }
            }

            // Regla 2: duplicación de droga genérica en tratamientos activos
            foreach (var activo in contexto.TratamientosActivos)
            {
                if (string.Equals(activo.DrogaGenerica, contexto.TratamientoPropuesto.DrogaGenerica,
                        StringComparison.OrdinalIgnoreCase))
                {
                    alertas.Add(new AlertaIADto
                    {
                        Tipo = TipoAlertaIA.Interaccion,
                        Severidad = SeveridadAlertaIA.Alta,
                        Descripcion = $"El paciente ya tiene un tratamiento activo con la misma droga genérica ({activo.DrogaGenerica})."
                    });
                }
            }

            // Regla 3: dosis "muy alta" (umbral arbitrario para demos)
            if (contexto.TratamientoPropuesto.Dosis > 1000m)
            {
                alertas.Add(new AlertaIADto
                {
                    Tipo = TipoAlertaIA.Dosis,
                    Severidad = SeveridadAlertaIA.Media,
                    Descripcion = "La dosis propuesta supera el umbral típico para la unidad indicada."
                });
            }

            var nivelRiesgo = CalcularNivelRiesgo(alertas);

            var analisis = new AnalisisIADto
            {
                NivelRiesgo = nivelRiesgo,
                AlertaDetectada = alertas.Count > 0,
                Alertas = alertas,
                Sugerencia = alertas.Count > 0 ? new SugerenciaIADto
                {
                    Aplicar = false,
                    Justificacion = "Mock: revisar manualmente las alertas detectadas antes de continuar."
                } : null,
                ResumenClinico = alertas.Count == 0
                    ? "Sin alertas detectadas por el motor de validación local."
                    : $"Se detectaron {alertas.Count} alerta(s) que requieren revisión médica."
            };

            var resultado = new AnalisisIAResultado
            {
                Analisis = analisis,
                PayloadJsonCrudo = JsonSerializer.Serialize(analisis, new JsonSerializerOptions
                {
                    WriteIndented = false,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }),
                Exitoso = true,
                FueTimeout = false
            };

            return Task.FromResult(resultado);
        }

        private static NivelRiesgoIA CalcularNivelRiesgo(List<AlertaIADto> alertas)
        {
            if (alertas.Count == 0) return NivelRiesgoIA.Bajo;
            var maxSeveridad = (int)SeveridadAlertaIA.Baja;
            foreach (var a in alertas)
                if ((int)a.Severidad > maxSeveridad) maxSeveridad = (int)a.Severidad;

            return maxSeveridad switch
            {
                (int)SeveridadAlertaIA.Critica => NivelRiesgoIA.Critico,
                (int)SeveridadAlertaIA.Alta => NivelRiesgoIA.Alto,
                (int)SeveridadAlertaIA.Media => NivelRiesgoIA.Medio,
                _ => NivelRiesgoIA.Bajo
            };
        }
    }
}