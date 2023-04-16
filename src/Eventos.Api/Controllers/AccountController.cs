using Eventos.IO.Application.VIEWMODELS;
using Eventos.IO.Domain.Core.Bus;
using Eventos.IO.Domain.Core.NOTIFICATIONS;
using Eventos.IO.Domain.INTERFACES;
using Eventos.IO.Domain.Organizadores.COMMANDS;
using Eventos.IO.Site.Areas.Identity.Pages.Account;
using Eventos.IO.Site.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Eventos.Api.Controllers;

public class AccountController : BaseController
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ILogger _logger;
    private readonly IBus _bus;

    public AccountController(UserManager<ApplicationUser> userManager,
                            SignInManager<ApplicationUser> signInManager,
                            ILoggerFactory loggerFactory,
                            IBus bus,
                            IDomainNotificationHandler<DomainNotification> domainNotification,
                            IUser user) : base(domainNotification, user, bus)
    {

        _userManager = userManager;
        _signInManager = signInManager;
        _bus = bus;

        _logger = loggerFactory.CreateLogger<AccountController>();
    }


    [HttpPost]
    [AllowAnonymous]
    [Route("nova-conta")]
    public async Task<IActionResult> Register([FromBody] OrganizadorViewModel model, int version)
    {
        if (version == 2)
        {
            return Response(new { Message = "API V2  não disponível" });
        }


        if (!ModelState.IsValid)
        {
            // NotificarErroModelInvalida();
            return Response(model);
        }



        var registroCommand = new RegistrarOrganizadorCommand(model.Id, model.Id, model.Nome, model.CPF, model.Email);
        _bus.SendCommand(registroCommand);



        _logger.LogInformation(1, "Usuario criado com sucesso");
        return Response(model);

    }


}



