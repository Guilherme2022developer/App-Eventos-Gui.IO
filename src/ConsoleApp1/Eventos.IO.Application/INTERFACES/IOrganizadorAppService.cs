using Eventos.IO.Application.VIEWMODELS;
using System;

namespace Eventos.IO.Application.INTERFACES
{
    public interface IOrganizadorAppService : IDisposable
    {
        void Registrar(OrganizadorViewModel organizadorViewModel);
    }
}