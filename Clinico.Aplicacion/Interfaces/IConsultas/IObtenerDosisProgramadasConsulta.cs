using Clinico.Dominio.Constantes;
using Clinico.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.Interfaces.IConsultas
{
    public interface IObtenerDosisProgramadasConsulta
    {
        Task<(List<TratamientoDosis> Elementos, int TotalRegistros)> ObtenerAsync(
            IReadOnlyCollection<Guid> pacientesIds,
            DateTime fecha,
            IReadOnlyCollection<EstadoDosis>? estados,
            int pagina,
            int tamPagina,
            DateTime? fechaHoraDesde,
            DateTime? fechaHoraHasta,
            CancellationToken cancellationToken);
    }
}