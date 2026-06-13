using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.DTOs.Respuestas
{
    public class ObtenerHistoriaClinicaRespuesta
    {
        public Guid HistoriaClinicaId { get; set; }

        public Guid PacienteId { get; set; }

        public string GrupoSanguineo { get; set; } = string.Empty;

        public string Alergias { get; set; } = string.Empty;

        public string Antecedentes { get; set; } = string.Empty;

        public List<DiagnosticoResumenRespuesta> Diagnosticos { get; set; } = new();

        public List<TratamientoActivoRespuesta> TratamientosActivos { get; set; } = new();
    }

    public class DiagnosticoResumenRespuesta
    {
        public Guid DiagnosticoId { get; set; }

        public string CodigoCie10 { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public DateTime FechaHora { get; set; }

        public string Observaciones { get; set; } = string.Empty;
    }

    public class TratamientoActivoRespuesta
    {
        public Guid TratamientoId { get; set; }

        public string Medicamento { get; set; } = string.Empty;

        public decimal Dosis { get; set; }

        public string UnidadMedida { get; set; } = string.Empty;

        public string Frecuencia { get; set; } = string.Empty;

        public DateTime FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        public string Estado { get; set; } = string.Empty;
    }
}
