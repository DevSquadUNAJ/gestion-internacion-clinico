using Clinico.Aplicacion.DTOs.Respuestas;
using Clinico.Aplicacion.DTOs.Respuestas.IA;
using Clinico.Aplicacion.DTOs.Solicitudes.IA;
using Clinico.Aplicacion.Interfaces.IMapeadores;
using Clinico.Dominio.Constantes;
using Clinico.Dominio.Entidades;
using System.Linq;

namespace Clinico.Aplicacion.Mapeadores
{
    public class PrescribirTratamientoMapeador : IPrescribirTratamientoMapeador
    {
        public ContextoClinicoIADto MapearContextoClinico(
            HistoriaClinica historiaClinica,
            Diagnostico diagnosticoActual,
            Medicamento medicamentoPropuesto,
            UnidadMedida unidadPropuesta,
            FrecuenciaAdministracion frecuenciaPropuesta,
            Tratamiento tratamientoPropuesto)
        {
            return new ContextoClinicoIADto
            {
                Paciente = new PacienteContextoIADto
                {
                    GrupoSanguineo = historiaClinica.GrupoSanguineo,
                    Alergias = historiaClinica.Alergias,
                    Antecedentes = historiaClinica.Antecedentes
                },
                DiagnosticoActual = new DiagnosticoContextoIADto
                {
                    CodigoCie10 = diagnosticoActual.CodigoCie10,
                    Descripcion = diagnosticoActual.CatalogoCie10.Descripcion,
                    FechaHora = diagnosticoActual.FechaHora
                },
                DiagnosticosPrevios = historiaClinica.Diagnosticos
                    .Where(d => d.Id != diagnosticoActual.Id)
                    .OrderByDescending(d => d.FechaHora)
                    .Select(d => new DiagnosticoContextoIADto
                    {
                        CodigoCie10 = d.CodigoCie10,
                        Descripcion = d.CatalogoCie10.Descripcion,
                        FechaHora = d.FechaHora
                    })
                    .ToList(),
                TratamientosActivos = historiaClinica.Diagnosticos
                    .SelectMany(d => d.Tratamientos)
                    .Where(t => t.Estado == EstadoTratamiento.Activo)
                    .Select(t => new TratamientoContextoIADto
                    {
                        Medicamento = t.Medicamento.NombreComercial,
                        DrogaGenerica = t.Medicamento.DrogaGenerica,
                        Dosis = t.Dosis,
                        Unidad = t.UnidadMedida.Abreviatura,
                        Frecuencia = t.FrecuenciaAdministracion.Descripcion
                    })
                    .ToList(),
                TratamientoPropuesto = new TratamientoPropuestoIADto
                {
                    Medicamento = medicamentoPropuesto.NombreComercial,
                    DrogaGenerica = medicamentoPropuesto.DrogaGenerica,
                    ViaAdministracion = medicamentoPropuesto.ViaAdministracion.ToString(),
                    Dosis = tratamientoPropuesto.Dosis,
                    Unidad = unidadPropuesta.Abreviatura,
                    Frecuencia = frecuenciaPropuesta.Descripcion,
                    FechaInicio = tratamientoPropuesto.FechaInicio,
                    FechaFin = tratamientoPropuesto.FechaFin,
                    Observaciones = tratamientoPropuesto.Observaciones
                }
            };
        }

        public PrescribirTratamientoRespuesta Mapear(
            Tratamiento tratamiento,
            AnalisisIADto? analisis,
            EstadoAnalisisIA estadoAnalisis)
        {
            return new PrescribirTratamientoRespuesta
            {
                TratamientoId = tratamiento.Id,
                Estado = tratamiento.Estado,
                FechaCreacion = tratamiento.FechaInicio, // Falta agregar fecha de creación en la entidad Tratamiento
                AnalisisIA = analisis,
                EstadoAnalisis = estadoAnalisis
            };
        }
    }
}