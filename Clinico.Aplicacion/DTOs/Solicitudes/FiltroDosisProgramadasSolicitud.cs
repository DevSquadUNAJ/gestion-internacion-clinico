using Clinico.Dominio.Constantes;
using System;
using System.Collections.Generic;

namespace Clinico.Aplicacion.DTOs.Solicitudes
{
    public class FiltroDosisProgramadasSolicitud
    {
        public DateTime Fecha { get; set; }
        public List<EstadoDosis>? Estados { get; set; }
        public Guid? PacienteId { get; set; }
        public Guid? SectorId { get; set; }
        public int Pagina { get; set; } = 1;
        public int TamPagina { get; set; } = 10;
    }
}