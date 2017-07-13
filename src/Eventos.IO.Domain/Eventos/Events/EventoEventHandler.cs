using Eventos.IO.Domain.Core.Events.Interfaces;

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
        }

        public void Handle(EventoAtualizadoEvent message)
        {
            // Enviar um email
            // E/ou fazer um log
        }

        public void Handle(EventoExcluidoEvent message)
        {
            // Enviar um email
            // E/ou fazer um log
        }
    }
}
