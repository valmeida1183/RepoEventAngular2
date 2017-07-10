using Eventos.IO.Domain.Core.Events;
using Eventos.IO.Domain.Core.Events.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Domain.Core.Notifications.Interfaces
{
    public interface IDomainNotificationHandler<T> : IHandler<T> where T : Message
    {
    }
}
