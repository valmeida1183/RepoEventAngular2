using Eventos.IO.Domain.Core.Models;
using System;

namespace Eventos.IO.Domain.Eventos
{
    public class Endereco : Entity<Endereco>
    {
        // Endereço é uma entidade que pertence a raiz de agregação Evento, nesse modelo de negócio somente um evento
        // possuí endereço, portanto a entidade endereço está dentro da pasta evento e é a raiz de agregação (evento)
        // quem trata de "endereço" e "organizador" é a classe Evento.

        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string CEP { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public Guid? EventoId { get; private set; }       

        //EF Propriedade de Navegação
        public virtual Evento Evento { get; private set; }

        public Endereco(Guid id, string logradouro, string numero, string complemento, string bairro,
           string cep, string cidade, string estado, Guid? eventoId)
        {
            Id = id; // nesse caso o Id é criado antes e passado no construtor (devido ao negócio)
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            CEP = cep;
            Cidade = cidade;
            Estado = estado;
            EventoId = eventoId;
        }

        //EF necessita de um construtor vazio
        protected Endereco()
        {

        }

        public override bool EhValido()
        {
            return true;
        }
    }
}