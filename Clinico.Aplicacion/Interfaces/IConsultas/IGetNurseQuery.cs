using Clinico.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.Interfaces.IConsultas
{
    public interface IGetNurseQuery
    {
        Task<Enfermera?> GetByIdAsync(
            Guid nurseId,
            CancellationToken cancellationToken);
    }
}