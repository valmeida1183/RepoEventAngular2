using Eventos.IO.Domain.Core.Commands;
using Eventos.IO.Domain.Core.Events;

namespace Eventos.IO.Domain.Core.Bus
{
    public interface IBus
    {
        void SendCommand<T>(T theCommand) where T : Command; // um comando sempre é enviado
        void RaiseEvent<T>(T theEvent) where T : Event; // um evento é sempre lançado
    }
}
