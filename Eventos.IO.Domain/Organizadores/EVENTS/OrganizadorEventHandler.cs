using Eventos.IO.Domain.Core.EVENTS;

namespace Eventos.IO.Domain.Organizadores.EVENTS;

public class OrganizadorEventHandler :
    IHandler<OrganizadorRegistradoEvent>
{
    public void Handle(OrganizadorRegistradoEvent message)
    {
        //TODO: Enviar um email?
    }
}