using Clinico.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinico.Infraestructura.Persistencia.Configuraciones
{
    public class ConfiguracionTratamiento : IEntityTypeConfiguration<Tratamiento>
    {
        public void Configure(EntityTypeBuilder<Tratamiento> builder)
        {
            builder.ToTable("Tratamientos");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Dosis)
                .HasPrecision(18, 2);

            builder.Property(t => t.Estado)
                .HasConversion<int>();

            builder.HasOne(t => t.Diagnostico)
                .WithMany(d => d.Tratamientos)
                .HasForeignKey(t => t.DiagnosticoId);

            builder.HasOne(t => t.Medicamento)
                .WithMany(m => m.Tratamientos)
                .HasForeignKey(t => t.MedicamentoId);

            builder.HasOne(t => t.UnidadMedida)
                .WithMany(um => um.Tratamientos)
                .HasForeignKey(t => t.UnidadMedidaId);

            builder.HasOne(t => t.FrecuenciaAdministracion)
                .WithMany(fa => fa.Tratamientos)
                .HasForeignKey(t => t.FrecuenciaAdministracionId);
        }
    }
}