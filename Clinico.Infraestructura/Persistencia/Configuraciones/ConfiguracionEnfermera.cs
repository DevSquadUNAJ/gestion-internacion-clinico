using System;
using Clinico.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinico.Infraestructura.Persistencia.Configuraciones
{
    public class ConfiguracionEnfermera : IEntityTypeConfiguration<Enfermera>
    {
        public void Configure(EntityTypeBuilder<Enfermera> builder)
        {
            builder.ToTable("Enfermeras");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(e => e.Legajo)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(e => e.Legajo)
                .IsUnique();

            builder.Property(e => e.SectorId)
                .IsRequired();

            builder.HasData(
                new Enfermera
                {
                    Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                    Nombre = "Enf. Rodrigo Godoy",
                    Legajo = "ENF-1001",
                    SectorId = Guid.Parse("99999999-9999-9999-9999-999999999999")
                },
                new Enfermera
                {
                    Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                    Nombre = "Enf. Matías Silva",
                    Legajo = "ENF-1002",
                    SectorId = Guid.Parse("99999999-9999-9999-9999-999999999999")
                }
            );
        }
    }
}