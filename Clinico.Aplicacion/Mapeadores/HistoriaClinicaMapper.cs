using Clinico.Aplicacion.DTOs.Respuestas;
using Clinico.Aplicacion.Interfaces.IMapeadores;
using Clinico.Dominio.Constantes;
using Clinico.Dominio.Entidades;
using System.Linq;

namespace Clinico.Aplicacion.Mapeadores
{
    public class HistoriaClinicaMapper : IHistoriaClinicaMapper
    {
        public ObtenerHistoriaClinicaRespuesta Mapear(HistoriaClinica historiaClinica)
        {
            return new ObtenerHistoriaClinicaRespuesta
            {
                HistoriaClinicaId = historiaClinica.Id,
                PacienteId = historiaClinica.PacienteId,
                // Solución a las líneas 18 y 19 (y agregamos antecedentes por seguridad):
                GrupoSanguineo = historiaClinica.GrupoSanguineo ?? string.Empty,
                Alergias = historiaClinica.Alergias ?? string.Empty,
                Antecedentes = historiaClinica.Antecedentes ?? string.Empty,

                Diagnosticos = historiaClinica.Diagnosticos
                    .OrderByDescending(d => d.FechaHora)
                    .Select(d => new DiagnosticoResumenRespuesta
                    {
                        DiagnosticoId = d.Id,
                        CodigoCie10 = d.CodigoCie10 ?? string.Empty,
                        // Solución a la línea 29 (usamos ?. por si CatalogoCie10 no vino en el Include):
                        Descripcion = d.CatalogoCie10?.Descripcion ?? string.Empty,
                        FechaHora = d.FechaHora,
                        Observaciones = d.Observaciones ?? string.Empty
                    })
                    .ToList(),

                TratamientosActivos = historiaClinica.Diagnosticos
                    .SelectMany(d => d.Tratamientos
                        .Where(t => t.Estado == EstadoTratamiento.Activo)
                        .Select(t => new TratamientoActivoRespuesta
                        {
                            TratamientoId = t.Id,
                            Medicamento = t.Medicamento?.NombreComercial ?? string.Empty,
                            Dosis = t.Dosis,
                            UnidadMedida = t.UnidadMedida?.Abreviatura ?? string.Empty,
                            Frecuencia = t.FrecuenciaAdministracion?.Descripcion ?? string.Empty,
                            FechaInicio = t.FechaInicio,
                            FechaFin = t.FechaFin,
                            Estado = t.Estado.ToString(),
                            CodigoCie10 = d.CodigoCie10 ?? string.Empty,
                            DescripcionDiagnostico = d.CatalogoCie10?.Descripcion ?? string.Empty,
                            FechaHoraDiagnostico = d.FechaHora
                        }))
                    .ToList()
            };
        }
    }
}