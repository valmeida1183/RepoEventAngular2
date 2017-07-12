using Eventos.IO.Domain.Core.Events;
using Eventos.IO.Domain.Core.Events.Interfaces;
using System.Collections.Generic;

namespace Eventos.IO.Domain.Core.Notifications.Interfaces
{
    public interface IDomainNotificationHandler<T> : IHandler<T> where T : Message
    {
        bool HasNotifications();
        List<T> GetNotifications();
    }
}
