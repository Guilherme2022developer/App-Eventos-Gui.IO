using System;
using Eventos.IO.Domain.Core.COMMANDS;

namespace Eventos.IO.Domain.INTERFACES
{
    public interface IUnitOfWork : IDisposable
    {
        CommandResponse Commit();
    }
}