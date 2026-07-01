using Clinico.Dominio.Entidades;
using System;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.Interfaces.IConsultas
{
    public interface IAuditoriaIAConsulta
    {
        Task<AuditoriaIA?> ObtenerUltimaPorTratamientoParaActualizarAsync(Guid tratamientoId);
    }
}