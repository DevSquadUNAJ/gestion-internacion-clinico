using Clinico.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class MedicoConfiguration
    : IEntityTypeConfiguration<Medico>
{
    public void Configure(EntityTypeBuilder<Medico> builder)
    {
        builder.ToTable("Medicos");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Nombre)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Matricula)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(x => x.Matricula)
            .IsUnique();

        builder.Property(x => x.Especialidad)
            .HasMaxLength(100)
            .IsRequired();
    }
}