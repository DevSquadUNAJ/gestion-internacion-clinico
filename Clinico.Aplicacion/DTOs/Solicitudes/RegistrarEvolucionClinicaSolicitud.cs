using System;

namespace Clinico.Aplicacion.DTOs.Solicitudes;

public class RegistrarEvolucionClinicaSolicitud
{
    public Guid HistoriaClinicaId { get; set; }

    public Guid MedicoId { get; set; }

    public string Observacion { get; set; } = string.Empty;
}