using Eventos.IO.Domain.Eventos;
using Eventos.IO.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Eventos.IO.Infra.Data.Mappings
{
    public class EventoMapping : EntityTypeConfiguration<Evento>
    {
        public override void Map(EntityTypeBuilder<Evento> builder)
        {
            builder.Property(e => e.Nome)
                    .HasColumnType("varchar(150)")
                    .IsRequired();

            builder.Property(e => e.DescricaoCurta)
                    .HasColumnType("varchar(150)");

            builder.Property(e => e.DescricaoLonga)
                    .HasColumnType("varchar(max)");

            builder.Property(e => e.NomeEmpresa)
                    .HasColumnType("varchar(150)")
                    .IsRequired();

            // Previne que essas propriedades virem colunas no banco.
            builder.Ignore(e => e.ValidationResult);

            builder.Ignore(e => e.Tags);

            builder.Ignore(e => e.CascadeMode);

            // Senpre é bomsetar o nome da tabela, principalmente se o nome for em português.
            builder.ToTable("Eventos");

            //Definição dos relacionamentos do banco.
            builder.HasOne(e => e.Organizador) // evento possui um organizador apenas.
                .WithMany(o => o.Eventos) // organizador possui muitos eventos.
                .HasForeignKey(e => e.OrganizadorId);

            builder.HasOne(e => e.Categoria)
                .WithMany(e => e.Eventos)
                .HasForeignKey(e => e.CategoriaId)
                .IsRequired(false);
        }
    }
}
