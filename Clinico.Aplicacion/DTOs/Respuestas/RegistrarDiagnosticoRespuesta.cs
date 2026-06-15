using System;

namespace Clinico.Aplicacion.DTOs.Respuestas;

public class RegistrarDiagnosticoRespuesta
{
    public Guid DiagnosticoId { get; set; }

    public DateTime FechaHora { get; set; }
}