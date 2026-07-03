using Clinico.Dominio.Constantes;
using Clinico.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Clinico.Infraestructura.Persistencia.Configuraciones
{
    public class ConfiguracionMedicamento : IEntityTypeConfiguration<Medicamento>
    {
        public void Configure(EntityTypeBuilder<Medicamento> builder)
        {
            builder.ToTable("Medicamentos");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.NombreComercial)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(m => m.DrogaGenerica)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(m => m.Presentacion)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(m => m.ViaAdministracion)
                .HasConversion<int>()
                .IsRequired();

            builder.HasData(
                new Medicamento
                {
                    Id = Guid.Parse("cccccccc-3333-3333-3333-cccccccccccc"),
                    NombreComercial = "Amoxidal",
                    DrogaGenerica = "Amoxicilina",
                    Presentacion = "Comprimidos",
                    ViaAdministracion = ViaAdministracion.Oral
                },
                new Medicamento
                {
                    Id = Guid.Parse("dddddddd-4444-4444-4444-dddddddddddd"),
                    NombreComercial = "Ibuprofeno",
                    DrogaGenerica = "Ibuprofeno",
                    Presentacion = "Comprimidos recubiertos",
                    ViaAdministracion = ViaAdministracion.Oral
                }
            );
        }
    }
}