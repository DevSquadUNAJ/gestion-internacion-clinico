using Clinico.Aplicacion.DTOs.Respuestas;
using Clinico.Dominio.Entidades;

namespace Clinico.Aplicacion.Interfaces.IMapeadores
{
    public interface IRegistrarDiagnosticoMapeador
    {
        RegistrarDiagnosticoRespuesta Mapear(Diagnostico diagnostico);
    }
}