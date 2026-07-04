using Clinico.Aplicacion.DTOs.Respuestas;
using Clinico.Aplicacion.DTOs.Respuestas.Admision;
using Clinico.Aplicacion.Interfaces.IMapeadores;

namespace Clinico.Aplicacion.Mapeadores
{
    public class SectorEnfermeraMapeador : ISectorEnfermeraMapeador
    {
        public SectorEnfermeraRespuesta Mapear(SectorOcupacionRespuesta sector)
        {
            return new SectorEnfermeraRespuesta
            {
                Nombre = sector.Nombre,
                Piso = sector.Piso
            };
        }
    }
}