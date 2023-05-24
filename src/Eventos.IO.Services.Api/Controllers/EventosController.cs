using AutoMapper;
using Eventos.IO.Application.INTERFACES;
using Eventos.IO.Application.VIEWMODELS;
using Eventos.IO.Domain.Core.Bus;
using Eventos.IO.Domain.Core.NOTIFICATIONS;
using Eventos.IO.Domain.Eventos.COMMANDS;
using Eventos.IO.Domain.Eventos.Repository;
using Eventos.IO.Domain.INTERFACES;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eventos.IO.Services.Api.Controllers;

public class EventosController : BaseController
{
    private readonly IEventoAppService _eventoAppService;
    private readonly IBus _bus;
    private readonly IEventoRepository _eventoRepository;
    private readonly IMapper _mapper;


    public EventosController(IDomainNotificationHandler<DomainNotification> domainNotification, IUser user, IBus bus, IEventoAppService eventoAppService, IEventoRepository eventoRepository, IMapper mapper) : base(domainNotification, user, bus)
    {
        _eventoAppService = eventoAppService;
        _bus = bus;
        _eventoRepository = eventoRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("eventos/obter-todos")]
    [AllowAnonymous]
    public IEnumerable<EventoViewModel> Get()
    {
        return _eventoAppService.ObterTodos();
    }


    [HttpGet]
    [Authorize]
    [Route("eventos/meus-eventos")]
    public IEnumerable<EventoViewModel> GetMeusEventos()
    {
        return _eventoAppService.ObterEventoPorOrganizador(OrganizadorId);

    }

    [HttpGet]
    [AllowAnonymous]
    [Route("eventos/{id:guid}")]
    public EventoViewModel Get(Guid id, int version)
    {
        return _eventoAppService.ObterEventoPorId(id);
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("eventos/obter-categorias")]
    public IEnumerable<CategoriaViewModel> ObterCategorias()
    {
        return _mapper.Map<IEnumerable<CategoriaViewModel>>(_eventoRepository.ObterCategorias());
    }

    [HttpPost]
    [Authorize] //TODO : colocar policy
    [Route("evento-registrar")]
    public IActionResult Post([FromBody] EventoViewModel eventoViewModel)
    {
        if (!ModelState.IsValid)
        {
            NotificarErroModelInvalida();
           return Response();
        }

        var eventoCommand = _mapper.Map<RegistrarEventoCommand>(eventoViewModel);

        _bus.SendCommand(eventoCommand);
        return Response(eventoCommand);
    }

    [HttpPut]
    [Authorize] //TODO : colocar policy
    [Route("evento-atualizar")]
    public IActionResult Put([FromBody] EventoViewModel eventoViewModel)
    {
        if (!ModelState.IsValid)
        {
            NotificarErroModelInvalida();
            return Response();
        }
        _eventoAppService.Atualizar(eventoViewModel);
        return Response(eventoViewModel);
    }


    [HttpDelete]
    [Authorize] //TODO : colocar policy
    [Route("evento-delete")]
    public IActionResult Delete(Guid id)
    {
        _eventoAppService.Excluir(id);
        return Response();
    }

}