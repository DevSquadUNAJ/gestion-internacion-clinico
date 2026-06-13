using Clinico.Aplicacion.DTOs.Respuestas;
using Clinico.Aplicacion.DTOs.Solicitudes;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.Interfaces.ICasosDeUso;

public interface IRegistrarDiagnosticoCasoDeUso
{
    Task<RegistrarDiagnosticoRespuesta> EjecutarAsync(RegistrarDiagnosticoSolicitud solicitud);
}