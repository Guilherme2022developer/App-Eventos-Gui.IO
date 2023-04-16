using Eventos.IO.Domain.Core.EVENTS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Domain.Core.NOTIFICATIONS
{
    public class DomainNotification : Event
    {
        public Guid DomainNotificationId { get; private set; }
        public string Key { get; private set; } //Nome do Evento
        public string Value { get; private set; } //Mensagem
        public int Version { get; private set; } //Versionamento de notificacao

        public DomainNotification(string key, string value)
        {
            DomainNotificationId = Guid.NewGuid();
            Key = key;
            Value = value;
            Version = 1;
        }
    }
}
