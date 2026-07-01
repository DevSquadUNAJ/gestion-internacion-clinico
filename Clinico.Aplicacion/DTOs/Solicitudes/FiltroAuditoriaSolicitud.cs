using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Clinico.Aplicacion.DTOs.Solicitudes
{
    public class FiltroAuditoriaSolicitud : IValidatableObject
    {
        // Paginación
        [Range(1, int.MaxValue, ErrorMessage = "El límite debe ser al menos 1.")]
        [DefaultValue(10)]
        public int Limit { get; set; } = 10;

        [Range(1, int.MaxValue, ErrorMessage = "La página debe ser al menos 1.")]
        [DefaultValue(1)]
        public int Page { get; set; } = 1;
        
        // Filtros
        public string UsuarioId { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public string Entidad { get; set; } = string.Empty;
        public Guid? EntidadId { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }

        // Validación de fechas
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FechaDesde.HasValue && FechaHasta.HasValue)
            {
                if (FechaDesde > FechaHasta)
                {
                    yield return new ValidationResult(
                        "La fecha de inicio no puede ser posterior a la fecha de fin.",
                        new[] { nameof(FechaDesde), nameof(FechaHasta) }
                    );
                }
            }
        }
    }
}
