using Eventos.IO.Domain.Core.COMMANDS;
using Eventos.IO.Domain.INTERFACES;
using Eventos.IO.Infra.Data.CONTEXT;

namespace Eventos.IO.Infra.Data.UOW;

public class UnitOfWork : IUnitOfWork
{
    private readonly EventosContext _context;

    public UnitOfWork(EventosContext context)
    {
        _context = context;
    }

    public CommandResponse Commit()
    {
        var rowsAffected = _context.SaveChanges();
        return new CommandResponse(rowsAffected > 0);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}