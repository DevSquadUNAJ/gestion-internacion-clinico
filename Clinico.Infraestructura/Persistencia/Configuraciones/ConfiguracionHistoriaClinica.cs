using Clinico.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinico.Infraestructura.Persistencia.Configuraciones
{
    public class ConfiguracionHistoriaClinica : IEntityTypeConfiguration<HistoriaClinica>
    {
        public void Configure(EntityTypeBuilder<HistoriaClinica> builder)
        {
            builder.ToTable("HistoriasClinicas");

            builder.HasKey(h => h.Id);

            builder.Property(h => h.GrupoSanguineo)
                .HasMaxLength(5);

            builder.Property(h => h.Alergias)
                .HasMaxLength(2000);

            builder.Property(h => h.Antecedentes)
                .HasMaxLength(4000);

            builder.Property(h => h.ObservacionesGenerales)
                .HasMaxLength(4000);
        }
    }
}