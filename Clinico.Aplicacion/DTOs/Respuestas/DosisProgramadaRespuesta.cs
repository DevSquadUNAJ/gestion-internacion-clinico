using Clinico.Dominio.Constantes;
using System;

namespace Clinico.Aplicacion.DTOs.Respuestas
{
    public sealed record DosisProgramadaRespuesta
    {
        public Guid DosisId { get; init; }
        public Guid PacienteId { get; init; }
        public string Paciente { get; init; } = string.Empty;
        public int NumeroCama { get; init; }
        public string Medicamento { get; init; } = string.Empty;
        public decimal Dosis { get; init; }
        public string UnidadMedida { get; init; } = string.Empty;
        public DateTime FechaProgramada { get; init; }
        public EstadoDosis Estado { get; init; }
        public PrioridadDosis Prioridad { get; init; }
    }
}