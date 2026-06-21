using Clinico.Dominio.Base;
using System;

namespace Clinico.Dominio.Entidades
{
    public class AuditoriaIA : EntidadBase
    {
        public Guid TratamientoId { get; set; }

        public bool AlertaDetectada { get; set; }

        public string MensajeIA { get; set; } = string.Empty;

        public bool FueForzado { get; set; }

        public string? JustificacionClinica { get; set; }

        public DateTime FechaHora { get; set; }

        public Tratamiento Tratamiento { get; set; } = null!;
    }
}
