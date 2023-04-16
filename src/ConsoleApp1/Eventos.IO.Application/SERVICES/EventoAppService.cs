﻿using AutoMapper;
using Eventos.IO.Application.INTERFACES;
using Eventos.IO.Application.VIEWMODELS;
using Eventos.IO.Domain.Core.Bus;
using Eventos.IO.Domain.Eventos.COMMANDS;
using Eventos.IO.Domain.Eventos.Repository;
using System.Collections.Generic;
using System;
using Eventos.IO.Domain.INTERFACES;

namespace Eventos.IO.Application.SERVICES
{
    public class EventoAppService : IEventoAppService
    {
        private readonly IBus _bus;
        private readonly IMapper _mapper;
        private readonly IEventoRepository _eventoRepository;
        private readonly IUser _user;

        public EventoAppService(IBus bus, IMapper mapper, IEventoRepository eventoRepository, IUser user)
        {
            _bus = bus;
            _mapper = mapper;
            _eventoRepository = eventoRepository;
            _user = user;
        }

        public void Registrar(EventoViewModel eventoViewModel)
        {
            var registroCommand = _mapper.Map<RegistrarEventoCommand>(eventoViewModel);
            _bus.SendCommand(registroCommand);
        }

        public void Atualizar(EventoViewModel eventoViewModel)
        {
            // TODO: Validar se o organizador é dono do evento
            var atualizarEventoCommand = _mapper.Map<AtualizarEventoCommand>(eventoViewModel);
            _bus.SendCommand(atualizarEventoCommand);
        }

       

        public void Excluir(Guid id)
        { _bus.SendCommand(new ExcluirEventoCommand(id)); }

        public void AdicionarEndereco(EnderecoViewModel enderecoViewModel)
        {
            var enderecoCommand = _mapper.Map<IncluirEnderecoEventoCommand>(enderecoViewModel);
            _bus.SendCommand(enderecoCommand);
        }

        public void AtualizarEndereco(EnderecoViewModel enderecoViewModel)
        {
            var enderecoCommand = _mapper.Map<AtualizarEnderecoEventoCommand>(enderecoViewModel);
            _bus.SendCommand(enderecoCommand);
        }

        public EnderecoViewModel ObterEnderecoPorId(Guid id)
        {
            return _mapper.Map<EnderecoViewModel>(_eventoRepository.ObterEnderecoPorId(id));
        }
        public void Dispose()
        { _eventoRepository.Dispose(); }

        public EventoViewModel ObterEventoPorId(Guid id)
        {
            //var evento = _eventoRepository.ObterPorId(id);

            //if (evento.OrganizadorId != _user.GetUserId())
            //{
                
            //}
            return _mapper.Map<EventoViewModel>(_eventoRepository.ObterPorId(id));
        }

        public IEnumerable<EventoViewModel> ObterEventoPorOrganizador(Guid organizadorId)
        { return _mapper.Map<IEnumerable<EventoViewModel>>(_eventoRepository.ObterEventoPorOrganizador(organizadorId)); }

        public IEnumerable<EventoViewModel> ObterTodos()
        { return _mapper.Map<IEnumerable<EventoViewModel>>(_eventoRepository.ObterTodos()); }
    }
}