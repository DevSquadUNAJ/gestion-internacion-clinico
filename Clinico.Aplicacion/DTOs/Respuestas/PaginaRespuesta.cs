using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.DTOs.Respuestas
{
    public sealed record PaginaRespuesta<T>
    {
        public IReadOnlyCollection<T> Elementos { get; init; } = [];

        public int PaginaActual { get; init; }

        public int TamPagina { get; init; }

        public int TotalRegistros { get; init; }

        public int TotalPaginas { get; init; }
    }
}
