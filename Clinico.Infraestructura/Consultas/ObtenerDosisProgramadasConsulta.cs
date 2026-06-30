using Clinico.Aplicacion.DTOs.Respuestas;
using Clinico.Aplicacion.Interfaces.IConsultas;
using Clinico.Dominio.Constantes;
using Clinico.Infraestructura.Persistencia;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clinico.Infraestructura.Consultas
{
    public sealed class ObtenerDosisProgramadasConsulta
        : IObtenerDosisProgramadasConsulta
    {
        private readonly ContextoBaseDeDatos _contexto;

        public ObtenerDosisProgramadasConsulta(
            ContextoBaseDeDatos contexto)
        {
            _contexto = contexto;
        }

        public async Task<PaginaRespuesta<DosisProgramadaRespuesta>>
            ObtenerAsync(
                Guid sectorId,
                DateTime fecha,
                int pagina,
                int tamPagina,
                CancellationToken cancellationToken)
        {
            var inicio = fecha.Date;
            var fin = inicio.AddDays(1);

            var consulta = _contexto.TratamientosDosis
                .Where(d =>
                    d.Estado == EstadoDosis.Pendiente &&
                    d.FechaProgramada >= inicio &&
                    d.FechaProgramada < fin);

            var totalRegistros =
                await consulta.CountAsync(cancellationToken);

            var elementos = await consulta
                .OrderBy(d => d.FechaProgramada)
                .Skip((pagina - 1) * tamPagina)
                .Take(tamPagina)
                .Select(d => new DosisProgramadaRespuesta
                {
                    DosisId = d.Id,
                    Paciente = d.Tratamiento
                        .Diagnostico
                        .HistoriaClinica
                        .PacienteId
                        .ToString(),

                    Medicamento = d.Tratamiento
                        .Medicamento
                        .NombreComercial,

                    FechaProgramada = d.FechaProgramada,

                    Estado = d.Estado.ToString(),

                    Prioridad =
                        d.FechaProgramada < DateTime.UtcNow
                            ? "Alta"
                            : "Normal"
                })
                .ToListAsync(cancellationToken);

            return new PaginaRespuesta<DosisProgramadaRespuesta>
            {
                Elementos = elementos,
                PaginaActual = pagina,
                TamPagina = tamPagina,
                TotalRegistros = totalRegistros,
                TotalPaginas = (int)Math.Ceiling(
                    totalRegistros / (double)tamPagina)
            };
        }
    }
}
