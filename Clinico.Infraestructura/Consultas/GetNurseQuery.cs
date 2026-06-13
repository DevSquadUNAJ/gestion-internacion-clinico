using Clinico.Aplicacion.Interfaces.IConsultas;
using Clinico.Dominio.Entidades;
using Clinico.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clinico.Infraestructura.Consultas
{
    public sealed class GetNurseQuery : IGetNurseQuery
    {
        private readonly ContextoBaseDeDatos _context;

        public GetNurseQuery(ContextoBaseDeDatos context)
        {
            _context = context;
        }

        public async Task<Enfermera?> GetByIdAsync(
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