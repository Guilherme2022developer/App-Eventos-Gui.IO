using Eventos.IO.Application.VIEWMODELS;
using System.Collections.Generic;
using System;

namespace Eventos.IO.Application.INTERFACES
{
    public interface IEventoAppService : IDisposable
    {
        void Registrar(EventoViewModel eventoViewModel);
        IEnumerable<EventoViewModel> ObterTodos();
        IEnumerable<EventoViewModel> ObterEventoPorOrganizador(Guid organizadorId);
        EventoViewModel ObterEventoPorId(Guid id);
        void Atualizar(EventoViewModel eventoViewModel);
        void Excluir(Guid id);

        void AdicionarEndereco(EnderecoViewModel enderecoViewModel);
        void AtualizarEndereco(EnderecoViewModel enderecoViewModel);
        EnderecoViewModel ObterEnderecoPorId(Guid id);
    }
}