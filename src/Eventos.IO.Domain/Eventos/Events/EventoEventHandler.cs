using Eventos.IO.Domain.Core.Events.Interfaces;
using System;

namespace Eventos.IO.Domain.Eventos.Events
{
    public class EventoEventHandler :
        IHandler<EventoRegistradoEvent>,
        IHandler<EventoAtualizadoEvent>,
        IHandler<EventoExcluidoEvent>
    {
        public void Handle(EventoRegistradoEvent message)
        {
            // Enviar um email
            // E/ou fazer um log
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Evento Registrado com Sucesso");
        }

        public void Handle(EventoAtualizadoEvent message)
        {
            // Enviar um email
            // E/ou fazer um log
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Evento Atualizado com Sucesso");
        }

        public void Handle(EventoExcluidoEvent message)
        {
            // Enviar um email
            // E/ou fazer um log
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Evento Excluído com Sucesso");
        }
    }
}
