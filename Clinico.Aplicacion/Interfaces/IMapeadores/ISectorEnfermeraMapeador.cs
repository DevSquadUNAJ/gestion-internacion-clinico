using Clinico.Aplicacion.DTOs.Respuestas;
using Clinico.Aplicacion.DTOs.Respuestas.Admision;

namespace Clinico.Aplicacion.Interfaces.IMapeadores
{
    public interface ISectorEnfermeraMapeador
    {
        SectorEnfermeraRespuesta Mapear(SectorOcupacionRespuesta sector);
    }
}