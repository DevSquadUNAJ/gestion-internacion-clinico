using Clinico.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class HistoriaClinicaConfiguration
    : IEntityTypeConfiguration<HistoriaClinica>
{
    public void Configure(EntityTypeBuilder<HistoriaClinica> builder)
    {
        builder.ToTable("HistoriasClinicas");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.GrupoSanguineo)
            .HasMaxLength(5);

        builder.Property(x => x.Alergias)
            .HasMaxLength(2000);

        builder.Property(x => x.Antecedentes)
            .HasMaxLength(4000);

        builder.Property(x => x.ObservacionesGenerales)
            .HasMaxLength(4000);
    }
}