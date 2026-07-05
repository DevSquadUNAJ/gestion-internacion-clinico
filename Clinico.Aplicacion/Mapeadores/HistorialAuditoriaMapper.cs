using Clinico.Aplicacion.DTOs.Respuestas;
using Clinico.Aplicacion.Interfaces.IMapeadores;
using Clinico.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinico.Aplicacion.Mapeadores
{
    public class HistorialAuditoriaMapper : IHistorialAuditoriaMapper
    {
        public ObtenerHistorialAuditoriaRespuesta Mapear(List<Auditoria> auditorias)
        {
            return new ObtenerHistorialAuditoriaRespuesta
            {
                Auditorias = auditorias.Select(a => new AuditoriaRespuesta
                {
                    UsuarioId = a.UsuarioId,
                    Rol = a.Rol,
                    Accion = a.Accion,
                    Entidad = a.Entidad,
                    EntidadId = a.EntidadId,
                    Descripcion = a.Descripcion ?? string.Empty,
                    PayloadJson = a.PayloadJson ?? string.Empty,
                    FechaHora = a.FechaHora
                }).ToList()
            };
        }
    }
}
