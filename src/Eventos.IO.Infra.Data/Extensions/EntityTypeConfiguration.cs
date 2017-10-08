using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventos.IO.Infra.Data.Extensions
{
    // No EF Core 2.0 não é mais necessária esta abordagem, veja: https://scottsauber.com/2017/09/11/customizing-ef-core-2-0-with-ientitytypeconfiguration
    public abstract class EntityTypeConfiguration<TEntity> where TEntity : class
    {
        public abstract void Map(EntityTypeBuilder<TEntity> builder);
    }
}
