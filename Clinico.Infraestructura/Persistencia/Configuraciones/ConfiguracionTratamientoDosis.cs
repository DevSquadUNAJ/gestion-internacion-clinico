using Clinico.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinico.Infraestructura.Persistencia.Configuraciones
{
    public class ConfiguracionTratamientoDosis : IEntityTypeConfiguration<TratamientoDosis>
    {
        public void Configure(EntityTypeBuilder<TratamientoDosis> builder)
        {
            builder.ToTable("TratamientosDosis");

            builder.HasKey(td => td.Id);

            builder.Property(td => td.Estado)
                .HasConversion<int>();

            builder.HasOne(td => td.Tratamiento)
                .WithMany(t => t.DosisProgramadas)
                .HasForeignKey(td => td.TratamientoId);

            builder.HasOne(td => td.Enfermera)
                .WithMany(e => e.TratamientosDosis)
                .HasForeignKey(td => td.EnfermeraId);
        }
    }
}