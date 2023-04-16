using Eventos.IO.Domain.Core.EVENTS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Domain.Eventos.EVENTS
{
    public class EventoEventHandler :
        IHandler<EventoRegistradoEvent>,
        IHandler<EventoAtualizadoEvent>,
        IHandler<EventoExcluidoEvent>,
        IHandler<EnderecoEventoAdicionadoEvent>,
        IHandler<EnderecoEventoAtualizadoEvent>
    {
        public void Handle(EventoExcluidoEvent message)
        {
            //Enviar Email
        }

        public void Handle(EventoAtualizadoEvent message)
        {
            //Enviar Email
        }

        public void Handle(EventoRegistradoEvent message)
        {
            //Enviar Email
        }

        public void Handle(EnderecoEventoAdicionadoEvent message)
        {
            //Enviar Email
        }

        public void Handle(EnderecoEventoAtualizadoEvent message)
        {
            //Enviar Email
        }
    }
}
