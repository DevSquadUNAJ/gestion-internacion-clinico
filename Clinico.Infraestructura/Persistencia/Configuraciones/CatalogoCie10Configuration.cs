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
    public class CatalogoCie10Configuration
        : IEntityTypeConfiguration<CatalogoCie10>
    {
        public void Configure(EntityTypeBuilder<CatalogoCie10> builder)
        {
            builder.ToTable("CatalogosCie10");

            builder.HasKey(x => x.Codigo);

            builder.Property(x => x.Codigo)
                .HasMaxLength(20);

            builder.Property(x => x.Descripcion)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(x => x.Categoria)
                .HasMaxLength(100);
        }
    }
}
