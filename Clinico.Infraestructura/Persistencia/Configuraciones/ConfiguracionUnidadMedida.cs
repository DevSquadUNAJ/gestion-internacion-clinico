using Clinico.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Clinico.Infraestructura.Persistencia.Configuraciones
{
    public class ConfiguracionUnidadMedida : IEntityTypeConfiguration<UnidadMedida>
    {
        public void Configure(EntityTypeBuilder<UnidadMedida> builder)
        {
            builder.ToTable("UnidadesMedida");

            builder.HasKey(um => um.Id);

            builder.Property(um => um.Nombre)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(um => um.Abreviatura)
                .HasMaxLength(10)
                .IsRequired();

            builder.HasData(
                new UnidadMedida
                {
                    Id = Guid.Parse("11111111-5555-5555-5555-111111111111"),
                    Nombre = "Miligramos",
                    Abreviatura = "mg"
                },
                new UnidadMedida
                {
                    Id = Guid.Parse("22222222-5555-5555-5555-222222222222"),
                    Nombre = "Mililitros",
                    Abreviatura = "ml"
                }
            );
        }
    }
}