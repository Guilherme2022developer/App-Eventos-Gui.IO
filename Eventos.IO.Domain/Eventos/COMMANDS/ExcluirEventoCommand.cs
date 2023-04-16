using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Domain.Eventos.COMMANDS
{
    public class ExcluirEventoCommand : BaseEventoCommand
    {
        public ExcluirEventoCommand(Guid id)
        {
            Id = id;
            AggregateId = Id;
        }
    }
}
