using System;
using System.Collections.Generic;

namespace Clinico.Aplicacion.DTOs.Solicitudes.IA
{
    public class ContextoClinicoIADto
    {
        public PacienteContextoIADto Paciente { get; set; } = new();
        public DiagnosticoContextoIADto DiagnosticoActual { get; set; } = new();
        public List<DiagnosticoContextoIADto> DiagnosticosPrevios { get; set; } = new();
        public List<TratamientoContextoIADto> TratamientosActivos { get; set; } = new();
        public TratamientoPropuestoIADto TratamientoPropuesto { get; set; } = new();
    }

    public class PacienteContextoIADto
    {
        public string GrupoSanguineo { get; set; } = string.Empty;
        public string? Alergias { get; set; }
        public string? Antecedentes { get; set; }
    }

    public class DiagnosticoContextoIADto
    {
        public string CodigoCie10 { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaHora { get; set; }
    }

    public class TratamientoContextoIADto
    {
        public string Medicamento { get; set; } = string.Empty;
        public string DrogaGenerica { get; set; } = string.Empty;
        public decimal Dosis { get; set; }
        public string Unidad { get; set; } = string.Empty;
        public string Frecuencia { get; set; } = string.Empty;
    }

    public class TratamientoPropuestoIADto
    {
        public string Medicamento { get; set; } = string.Empty;
        public string DrogaGenerica { get; set; } = string.Empty;
        public string ViaAdministracion { get; set; } = string.Empty;
        public decimal Dosis { get; set; }
        public string Unidad { get; set; } = string.Empty;
        public string Frecuencia { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string? Observaciones { get; set; }
    }
}