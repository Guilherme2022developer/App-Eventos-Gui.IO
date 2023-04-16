using Eventos.IO.Domain.Core.EVENTS;
using System.Collections.Generic;

namespace Eventos.IO.Domain.Core.NOTIFICATIONS
{
    public interface IDomainNotificationHandler<T> : IHandler<T> where T : Message
    {
        bool HasNotifications();
        List<T> GetNotifications();
    }
}