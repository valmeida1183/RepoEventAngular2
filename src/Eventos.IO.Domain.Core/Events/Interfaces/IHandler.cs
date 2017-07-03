using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Domain.Core.Events.Interfaces
{
    // <in T> é o conceito de "contra variante" que diz que "T" pode ser mais ou menos derivado de Message
    public interface IHandler<in T> where T : Message
    {
        void Handle(T message);
    }
}
