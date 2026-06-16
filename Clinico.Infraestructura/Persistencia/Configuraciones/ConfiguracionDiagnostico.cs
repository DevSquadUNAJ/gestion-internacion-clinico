using Clinico.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinico.Infraestructura.Persistencia.Configuraciones
{
    public class ConfiguracionDiagnostico : IEntityTypeConfiguration<Diagnostico>
    {
        public void Configure(EntityTypeBuilder<Diagnostico> builder)
        {
            builder.ToTable("Diagnosticos");

            builder.HasKey(d => d.Id);

            builder.HasOne(d => d.HistoriaClinica)
                .WithMany(h => h.Diagnosticos)
                .HasForeignKey(d => d.HistoriaClinicaId);

            builder.HasOne(d => d.Medico)
                .WithMany(m => m.Diagnosticos)
                .HasForeignKey(d => d.MedicoId);

            builder.HasOne(d => d.CatalogoCie10)
                .WithMany(c => c.Diagnosticos)
                .HasForeignKey(d => d.CodigoCie10);

            builder.Property(d => d.Observaciones)
                .HasMaxLength(4000);
        }
    }
}