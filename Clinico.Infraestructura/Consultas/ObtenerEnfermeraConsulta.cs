using Clinico.Aplicacion.Interfaces.IConsultas;
using Clinico.Dominio.Entidades;
using Clinico.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clinico.Infraestructura.Consultas
{
    public sealed class ObtenerEnfermeraConsulta : IObtenerEnfermeraConsulta
    {
        private readonly ContextoBaseDeDatos _contexto;

        public ObtenerEnfermeraConsulta(ContextoBaseDeDatos contexto)
        {
            _contexto = contexto;
        }

        public async Task<Enfermera?> ObtenerPorIdAsync(
            Guid enfermeraId,
            CancellationToken cancellationToken)
        {
            return await _contexto.Enfermeras
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    e => e.Id == enfermeraId,
                    cancellationToken);
        }
    }
}