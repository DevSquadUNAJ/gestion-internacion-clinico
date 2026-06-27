using System;

namespace Clinico.Aplicacion.DTOs.Solicitudes
{
    public class ConfirmarTratamientoSolicitud
    {
        public Guid MedicamentoId { get; set; }
        public Guid UnidadMedidaId { get; set; }
        public Guid FrecuenciaAdministracionId { get; set; }
        public decimal Dosis { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string? Observaciones { get; set; }
        public bool FueForzado { get; set; }
        public string? JustificacionClinica { get; set; }
    }
}