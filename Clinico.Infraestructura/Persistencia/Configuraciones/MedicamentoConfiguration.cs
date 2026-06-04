using Clinico.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinico.Infraestructura.Persistencia.Configuraciones
{
    public class MedicamentoConfiguration
        : IEntityTypeConfiguration<Medicamento>
    {
        public void Configure(EntityTypeBuilder<Medicamento> builder)
        {
            builder.ToTable("Medicamentos");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.NombreComercial)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.DrogaGenerica)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Presentacion)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.ViaAdministracion)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
