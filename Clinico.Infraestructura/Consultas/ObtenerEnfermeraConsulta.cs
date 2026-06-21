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
        private readonly ContextoBaseDeDatos _context;

        public ObtenerEnfermeraConsulta(ContextoBaseDeDatos context)
        {
            _context = context;
        }

        public async Task<Enfermera?> ObtenerPorIdAsync(
            Guid nurseId,
            CancellationToken cancellationToken)
        {
            return await _context.Enfermeras
                .FirstOrDefaultAsync(
                    e => e.Id == nurseId,
                    cancellationToken);
        }
    }
}