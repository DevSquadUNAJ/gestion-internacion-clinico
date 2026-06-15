using Clinico.Aplicacion.DTOs.Respuestas;
using Clinico.Aplicacion.DTOs.Solicitudes;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.Interfaces.ICasosDeUso;

public interface IRegistrarEvolucionClinicaCasoDeUso
{
    Task<RegistrarEvolucionClinicaRespuesta> EjecutarAsync(RegistrarEvolucionClinicaSolicitud solicitud);
}