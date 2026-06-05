using Clinico.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinico.Infraestructura.Persistencia.Configuraciones
{
    public class TratamientoConfiguration
        : IEntityTypeConfiguration<Tratamiento>
    {
        public void Configure(EntityTypeBuilder<Tratamiento> builder)
        {
            builder.ToTable("Tratamientos");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Dosis)
                .HasPrecision(18, 2);

            builder.Property(x => x.Estado)
                .HasConversion<int>();

            builder.HasOne(x => x.Diagnostico)
                .WithMany(x => x.Tratamientos)
                .HasForeignKey(x => x.DiagnosticoId);

            builder.HasOne(x => x.Medicamento)
                .WithMany(x => x.Tratamientos)
                .HasForeignKey(x => x.MedicamentoId);

            builder.HasOne(x => x.UnidadMedida)
                .WithMany(x => x.Tratamientos)
                .HasForeignKey(x => x.UnidadMedidaId);

            builder.HasOne(x => x.FrecuenciaAdministracion)
                .WithMany(x => x.Tratamientos)
                .HasForeignKey(x => x.FrecuenciaAdministracionId);
        }
    }
}
