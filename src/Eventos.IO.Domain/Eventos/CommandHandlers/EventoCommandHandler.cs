﻿using Eventos.IO.Domain.CommandHandlers;
using Eventos.IO.Domain.Core.Events.Interfaces;
using Eventos.IO.Domain.Eventos.Commands;
using Eventos.IO.Domain.Eventos.Repository;
using Eventos.IO.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Domain.Eventos.CommandHandlers
{
    public class EventoCommandHandler : CommandHandler,
        IHandler<RegistrarEventoCommand>, 
        IHandler<AtualizarEventoCommand>, 
        IHandler<ExcluirEventoCommand>
    {
        private readonly IEventoRepository _eventoRepository;

        public EventoCommandHandler(IEventoRepository eventoRepository, IUnitOfWork uow): base(uow)
        {
            _eventoRepository = eventoRepository;
        }

        public void Handle(RegistrarEventoCommand message)
        {
            var evento = new Evento(message.Nome, message.DataInicio, message.DataFim, message.Gratuito, message.Valor, message.Online, message.NomeEmpresa);

            if (!evento.EhValido())
            {
                NotificarValidacoesErro(evento.ValidationResult);
                return;
            }

            //TODO:
            // Validações de negócio!
            // Organizador pode registrar evento?

            //Persistência
            _eventoRepository.Add(evento);

            if (Commit())
            {
                Console.WriteLine("Evento registrado com sucesso");
            }
        }

        public void Handle(AtualizarEventoCommand message)
        {
            throw new NotImplementedException();
        }

        public void Handle(ExcluirEventoCommand message)
        {
            throw new NotImplementedException();
        }
    }
}
