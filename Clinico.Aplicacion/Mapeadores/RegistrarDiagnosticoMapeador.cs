using Clinico.Aplicacion.DTOs.Respuestas;
using Clinico.Aplicacion.Interfaces.IMapeadores;
using Clinico.Dominio.Entidades;

namespace Clinico.Aplicacion.Mapeadores
{
    public class RegistrarDiagnosticoMapeador : IRegistrarDiagnosticoMapeador
    {
        public RegistrarDiagnosticoRespuesta Mapear(Diagnostico diagnostico)
        {
            return new RegistrarDiagnosticoRespuesta
            {
                DiagnosticoId = diagnostico.Id,
                FechaHora = diagnostico.FechaHora
            };
        }
    }
}