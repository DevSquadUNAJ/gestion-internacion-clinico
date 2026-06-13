using Clinico.Aplicacion.DTOs.Respuestas;
using Clinico.Aplicacion.Interfaces.IMapeadores;
using Clinico.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.Mapeadores
{
    public class HistoriaClinicaMapper
    : IHistoriaClinicaMapper
    {
        public ObtenerHistoriaClinicaRespuesta Mapear(
            HistoriaClinica historiaClinica)
        {
            return new ObtenerHistoriaClinicaRespuesta
            {
                HistoriaClinicaId = historiaClinica.Id,
                PacienteId = historiaClinica.PacienteId,
                GrupoSanguineo = historiaClinica.GrupoSanguineo,
                Alergias = historiaClinica.Alergias,
                Antecedentes = historiaClinica.Antecedentes,

                Diagnosticos = historiaClinica.Diagnosticos
                    .OrderByDescending(d => d.FechaHora)
                    .Select(d => new DiagnosticoResumenRespuesta
                    {
                        DiagnosticoId = d.Id,
                        CodigoCie10 = d.CodigoCie10,
                        Descripcion = d.CatalogoCie10.Descripcion,
                        FechaHora = d.FechaHora,
                        Observaciones = d.Observaciones
                    })
                    .ToList(),

                TratamientosActivos = historiaClinica.Diagnosticos
                    .SelectMany(d => d.Tratamientos)
                    .Where(t => t.Estado == EstadoTratamiento.Activo)
                    .Select(t => new TratamientoActivoRespuesta
                    {
                        TratamientoId = t.Id,
                        Medicamento = t.Medicamento.NombreComercial,
                        Dosis = t.Dosis,
                        UnidadMedida = t.UnidadMedida.Abreviatura,
                        Frecuencia = t.FrecuenciaAdministracion.Descripcion,
                        FechaInicio = t.FechaInicio,
                        FechaFin = t.FechaFin,
                        Estado = t.Estado.ToString(),
                    })
                    .ToList()
            };
        }
    }
}
