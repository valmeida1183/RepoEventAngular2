using Eventos.IO.Domain.Core.Bus;
using Eventos.IO.Domain.Core.Commands;
using Eventos.IO.Domain.Core.Events;

namespace Eventos.IO.Infra.CrossCutting.Bus
{
    // Sealed -> Não pode ser herdada
    public sealed class InMemoryBus : IBus
    {
        public void RaiseEvent<T>(T theEvent) where T : Event
        {
            throw new System.NotImplementedException();
        }

        public void SendCommand<T>(T theCommand) where T : Command
        {
            throw new System.NotImplementedException();
        }


    }
}
