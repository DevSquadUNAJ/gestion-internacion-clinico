using Clinico.Aplicacion.Interfaces.IConsultas;
using Clinico.Dominio.Entidades;
using Clinico.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinico.Infraestructura.Consultas
{

    public sealed class ObtenerTratamientoDosisConsulta
        : IObtenerTratamientoDosisConsulta
    {
        private readonly ContextoBaseDeDatos _contexto;

        public ObtenerTratamientoDosisConsulta(
            ContextoBaseDeDatos contexto)
        {
            _contexto = contexto;
        }

        public async Task<TratamientoDosis?> ObtenerPorIdAsync(
            Guid dosisId,
            CancellationToken cancellationToken)
        {
            return await _contexto.TratamientosDosis
                .FirstOrDefaultAsync(
                    d => d.Id == dosisId,
                    cancellationToken);
        }
    }
}
