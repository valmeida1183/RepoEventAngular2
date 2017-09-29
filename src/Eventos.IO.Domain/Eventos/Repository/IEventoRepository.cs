using Eventos.IO.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Eventos.IO.Domain.Eventos.Repository
{
    public interface IEventoRepository : IRepository<Evento>
    {
        IEnumerable<Evento> ObterEventoPorOrganizador(Guid organizadorId);
        Endereco ObterEnderecoPorId(Guid Id);
        // Lembrando: Uma das regras da agregação é que temos que ter UM repositório por agregação.
        void AdicionarEndereco(Endereco endereco);
        void AtualizarEndereco(Endereco endereco);

    }
}
