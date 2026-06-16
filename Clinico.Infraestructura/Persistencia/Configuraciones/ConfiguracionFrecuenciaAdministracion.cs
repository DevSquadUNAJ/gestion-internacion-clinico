using Clinico.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Clinico.Infraestructura.Persistencia.Configuraciones
{
    public class ConfiguracionFrecuenciaAdministracion : IEntityTypeConfiguration<FrecuenciaAdministracion>
    {
        public void Configure(EntityTypeBuilder<FrecuenciaAdministracion> builder)
        {
            builder.ToTable("FrecuenciasAdministracion");

            builder.HasKey(fa => fa.Id);

            builder.Property(fa => fa.Descripcion)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(fa => fa.CantidadHoras)
                .IsRequired();

            builder.HasData(
                new FrecuenciaAdministracion
                {
                    Id = Guid.Parse("33333333-6666-6666-6666-333333333333"),
                    Descripcion = "Cada 8 horas",
                    CantidadHoras = 8
                },
                new FrecuenciaAdministracion
                {
                    Id = Guid.Parse("44444444-6666-6666-6666-444444444444"),
                    Descripcion = "Cada 12 horas",
                    CantidadHoras = 12
                }
            );
        }
    }
}