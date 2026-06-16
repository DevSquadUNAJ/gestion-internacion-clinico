using Clinico.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}