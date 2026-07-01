using Clinico.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.Interfaces.IConsultas
{
    public interface ITratamientoDosisConsulta
    {
        Task<List<TratamientoDosis>> ObtenerPorTratamientoAsync(Guid tratamientoId);
        Task<IEnumerable<TratamientoDosis>> ObtenerPendientesPorPacientesAsync(IEnumerable<Guid> pacientesIds);
    }
}
