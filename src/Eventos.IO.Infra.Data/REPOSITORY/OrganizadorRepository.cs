using Eventos.IO.Domain.Organizadores.REPOSITORY;
using Eventos.IO.Domain.Organizadores;
using Eventos.IO.Infra.Data.CONTEXT;

namespace Eventos.IO.Infra.Data.REPOSITORY;

public class OrganizadorRepository : Repository<Organizador>, IOrganizadorRepository
{
    public OrganizadorRepository(EventosContext context) : base(context)
    {
    }
}