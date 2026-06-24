using Clinico.Aplicacion.DTOs.Respuestas;
using Clinico.Aplicacion.DTOs.Respuestas.IA;
using Clinico.Aplicacion.DTOs.Solicitudes.IA;
using Clinico.Dominio.Constantes;
using Clinico.Dominio.Entidades;
using System.Collections.Generic;

namespace Clinico.Aplicacion.Interfaces.IMapeadores
{
    public interface IPrescribirTratamientoMapeador
    {
        ContextoClinicoIADto MapearContextoClinico(
            HistoriaClinica historiaClinica,
            Diagnostico diagnosticoActual,
            Medicamento medicamentoPropuesto,
            UnidadMedida unidadPropuesta,
            FrecuenciaAdministracion frecuenciaPropuesta,
            Tratamiento tratamientoPropuesto);

        PrescribirTratamientoRespuesta Mapear(
            Tratamiento tratamiento,
            AnalisisIADto? analisis,
            EstadoAnalisisIA estadoAnalisis);
    }
}