using Eventos.IO.Domain.Core.COMMANDS;
using Eventos.IO.Domain.Core.EVENTS;

namespace Eventos.IO.Domain.Core.Bus
{
    public interface IBus
    {
        void SendCommand<T>(T theCommand) where T : Command;
        void RaiseEvent<T>(T theEvent) where T : Event;
    }
}
