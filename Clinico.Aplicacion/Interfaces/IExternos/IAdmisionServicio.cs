using System;
using System.Threading.Tasks;
using Clinico.Aplicacion.DTOs.Respuestas.Admision;

namespace Clinico.Aplicacion.Interfaces.IExternos
{
    public interface IAdmisionServicio
    {
        Task<ContextoInternacionRespuesta> ObtenerContextoInternacionAsync(Guid internacionId);
    }
}