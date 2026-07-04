using System;

namespace Clinico.Aplicacion.DTOs.Respuestas.Admision
{
    public class SectorOcupacionRespuesta
    {
        public Guid SectorId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Piso { get; set; }
        public int CantidadTotalCamas { get; set; }
        public int CantidadCamasDisponibles { get; set; }
        public int CantidadCamasOcupadas { get; set; }
        public double PorcentajeOcupacion { get; set; }
    }
}