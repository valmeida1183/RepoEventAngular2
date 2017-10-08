using Eventos.IO.Domain.Core.Models;
using Eventos.IO.Domain.Interfaces;
using Eventos.IO.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eventos.IO.Infra.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity<TEntity>
    {
        protected EventosContext Db;
        protected DbSet<TEntity> DbSet;

        public Repository(EventosContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public virtual void Adicionar(TEntity obj)
        {
            DbSet.Add(obj);
        }

        public virtual void Atualizar(TEntity obj)
        {
            DbSet.Update(obj);
        }

        public virtual IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.AsNoTracking().Where(predicate);
        }       

        public virtual TEntity ObterPorId(Guid id)
        {
            // poderia ser também "DbSet.Find(id)", mas dae não seria possível utilizar o "AsNoTracking" que tem performance superior.
           return DbSet.AsNoTracking().FirstOrDefault(t => t.Id == id);
        }

        public virtual IEnumerable<TEntity> ObterTodos()
        {
            return DbSet.ToList();
        }

        public virtual void Remover(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public int SaveChanges()
        {
            // Retorna o número de linhas afetadas.
           return Db.SaveChanges();
        }

        public void Dispose()
        {
            // Acaba com a instância do contexto.
            Db.Dispose();
        }
    }
}
