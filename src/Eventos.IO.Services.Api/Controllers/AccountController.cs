using Eventos.IO.Application.VIEWMODELS;
using Eventos.IO.Domain.Core.Bus;
using Eventos.IO.Domain.Core.NOTIFICATIONS;
using Eventos.IO.Domain.INTERFACES;
using Eventos.IO.Domain.Organizadores.COMMANDS;
using Eventos.IO.Services.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Eventos.IO.Services.Api.Controllers;

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
            return Response(new {Message = "API V2  não disponível" });
        }


        if (!ModelState.IsValid)
        {
           // NotificarErroModelInvalida();
            return Response(model);
        }
           

       
        var  registroCommand = new RegistrarOrganizadorCommand(model.Id, model.Nome,model.CPF,model.Email);
        _bus.SendCommand(registroCommand);

        return Response(model);


    }

    //[HttpPost]
    //[AllowAnonymous]
    //[Route("conta")]
    //public async Task<IActionResult> Login([FromBody] LoginModel model)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        NotificarErroModelInvalida();
    //        return Response(model.Input);

    //    }

    //    var result = await _signInManager.PasswordSignInAsync(model.Input.Email, model.Input.Password,false,lockoutOnFailure: true);

    //    if (result.Succeeded)
    //    {
    //        _logger.LogInformation(1,"Usurio logado  com sucesso");
    //        return Response(model.Input);
    //    }

    //    NotificarErro(result.ToString(),"Falha ao realizar o login");
    //    return Response(model.Input);
    //}

}