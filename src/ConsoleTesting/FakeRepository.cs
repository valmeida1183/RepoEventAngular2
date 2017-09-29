using Eventos.IO.Domain.Eventos;
using System;
using Eventos.IO.Domain.Eventos.Repository;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ConsoleTesting
{
    public class FakeRepository : IEventoRepository
    {
        public void Adicionar(Evento obj)
        {
            //
        }

        public void Dispose()
        {
            //
        }

        public IEnumerable<Evento> Buscar(Expression<Func<Evento, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Evento> ObterTodos()
        {
            throw new NotImplementedException();
        }

        public Evento ObterPorId(Guid id)
        {
            return new Evento("Fake", DateTime.Now, DateTime.Now, true, 0, true, "Empresa");
        }

        public void Remover(Guid Id)
        {
            //
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Atualizar(Evento obj)
        {
            //
        }

        public IEnumerable<Evento> ObterEventoPorOrganizador(Guid organizadorId)
        {
            throw new NotImplementedException();
        }

        public Endereco ObterEnderecoPorId(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void AdicionarEndereco(Endereco endereco)
        {
            throw new NotImplementedException();
        }

        public void AtualizarEndereco(Endereco endereco)
        {
            throw new NotImplementedException();
        }
    }
}