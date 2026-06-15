using System;

namespace Clinico.Aplicacion.DTOs.Solicitudes
{
    public class ModificarTratamientoSolicitud
    {
        public decimal Dosis { get; set; }

        public Guid FrecuenciaAdministracionId { get; set; }

        public bool Suspender { get; set; }
    }
}
