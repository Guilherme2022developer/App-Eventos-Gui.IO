﻿using Eventos.IO.Domain.INTERFACES;
using System.Collections.Generic;
using System;

namespace Eventos.IO.Domain.Eventos.Repository
{
    public interface IEventoRepository : IRepository<Evento>
    {
        IEnumerable<Evento> ObterEventoPorOrganizador(Guid organizadorId);
        Endereco ObterEnderecoPorId(Guid id);
        void AdicionarEndereco(Endereco endereco);
        void AtualizarEndereco(Endereco endereco);
        IEnumerable<Categoria> ObterCategorias();
    }
}