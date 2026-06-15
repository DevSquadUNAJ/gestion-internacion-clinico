using System;

namespace Clinico.Aplicacion.DTOs.Respuestas;

public class RegistrarEvolucionClinicaRespuesta
{
    public Guid EvolucionClinicaId { get; set; }

    public DateTime FechaHora { get; set; }
}