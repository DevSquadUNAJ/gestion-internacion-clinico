using Clinico.Dominio.Base;
using Clinico.Dominio.Constantes;
using System;
using System.Collections.Generic;

namespace Clinico.Dominio.Entidades
{
    public class Tratamiento : EntidadBase
    {
        public Guid DiagnosticoId { get; set; }

        public Guid MedicamentoId { get; set; }

        public Guid UnidadMedidaId { get; set; }

        public Guid FrecuenciaAdministracionId { get; set; }

        public decimal Dosis { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public EstadoTratamiento Estado { get; set; }

        public string? Observaciones { get; set; }

        public Diagnostico Diagnostico { get; set; } = null!;

        public Medicamento Medicamento { get; set; } = null!;

        public UnidadMedida UnidadMedida { get; set; } = null!;

        public FrecuenciaAdministracion FrecuenciaAdministracion { get; set; } = null!;

        public ICollection<TratamientoDosis> DosisProgramadas { get; set; } = new List<TratamientoDosis>();

        public ICollection<AuditoriaIA> AuditoriasIA { get; set; } = new List<AuditoriaIA>();
    }
}
