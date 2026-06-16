using Clinico.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinico.Infraestructura.Persistencia.Configuraciones
{
    public class ConfiguracionCatalogoCie10 : IEntityTypeConfiguration<CatalogoCie10>
    {
        public void Configure(EntityTypeBuilder<CatalogoCie10> builder)
        {
            builder.ToTable("CatalogosCie10");

            builder.HasKey(c => c.Codigo);

            builder.Property(c => c.Codigo)
                .HasMaxLength(20);

            builder.Property(c => c.Descripcion)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(c => c.Categoria)
                .HasMaxLength(100);
        }
    }
}