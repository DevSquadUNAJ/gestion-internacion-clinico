using Clinico.Dominio.Entidades;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.Interfaces.IConsultas
{
    public interface IObtenerTratamientoDosisConsulta
    {
        Task<TratamientoDosis?> ObtenerPorIdAsync(
            Guid dosisId,
            CancellationToken cancellationToken);
    }
}
