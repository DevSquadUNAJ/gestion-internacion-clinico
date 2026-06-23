using Clinico.Aplicacion.Interfaces.IConsultas;
using Clinico.Dominio.Entidades;
using Clinico.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clinico.Infraestructura.Consultas
{
    public sealed class ObtenerTratamientoDosisConsulta : IObtenerTratamientoDosisConsulta
    {
        private readonly ContextoBaseDeDatos _contexto;

        public ObtenerTratamientoDosisConsulta(ContextoBaseDeDatos contexto)
        {
            _contexto = contexto;
        }

        public async Task<TratamientoDosis?> ObtenerPorIdAsync(
            Guid dosisId,
            CancellationToken cancellationToken)
        {
            // SIN AsNoTracking() porque esta entidad luego mutará su estado y se actualizará en BD
            return await _contexto.TratamientosDosis
                .FirstOrDefaultAsync(
                    d => d.Id == dosisId,
                    cancellationToken);
        }
    }
}