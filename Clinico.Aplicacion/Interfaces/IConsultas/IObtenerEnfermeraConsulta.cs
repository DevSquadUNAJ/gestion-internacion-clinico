using Clinico.Dominio.Entidades;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.Interfaces.IConsultas
{
    public interface IObtenerEnfermeraConsulta
    {
        Task<Enfermera?> ObtenerPorIdAsync(
            Guid enfermeraId,
            CancellationToken cancellationToken);
    }
}