using Eventos.IO.Application.VIEWMODELS;
using Eventos.IO.Domain.Core.Bus;
using Eventos.IO.Domain.Core.NOTIFICATIONS;
using Eventos.IO.Domain.INTERFACES;
using Eventos.IO.Services.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Eventos.IO.Services.Api.Controllers;

public class AuthController : BaseController
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    public AuthController(IDomainNotificationHandler<DomainNotification> domainNotification, IUser user, IBus bus, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager) : base(domainNotification, user, bus)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("registrar-nova-conta")]
    public async Task<IActionResult> Registrar([FromBody] RegistrerUserViewmodel registrerUser, int version)
    {

        if (version == 2)
        {
            return Response(new { Message = "API V2  não disponível" });
        }

        if (!ModelState.IsValid)
        {
            NotificarErroModelInvalida();
            return Response();
        }


        var user = new ApplicationUser
        {
            UserName = registrerUser.Email,
            Email = registrerUser.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, registrerUser.Password);

        if (result.Succeeded)
        {
            var token = UserService.GenereteToken(registrerUser);
            registrerUser.Token = token;
            registrerUser.Password = null;

            return Response(registrerUser);
        }

        foreach (var error in result.Errors)
        {
            NotificarErro(result.ToString(), error.Description);
        }

        return Response(registrerUser);
    }
    [HttpPost]
    [AllowAnonymous]
    [Route("entrar")]
    public async Task<IActionResult> Logar(LoginUserViewModel logarModel)
    {
        if (!ModelState.IsValid)
        {
            NotificarErroModelInvalida();
            return Response();
        }

        var result = await _signInManager.PasswordSignInAsync(logarModel.Email, logarModel.Password, false, true);

        if (result.Succeeded)
        {
             var token = UserService.GenereteTokenEntrar(logarModel);
             logarModel.Token = token;
            return Response(logarModel);
        }

        if (result.IsLockedOut)
        {
            return Response("Usuario bloqueado");
        }

        NotificarErro("12","Senha ou usuario incorretos");
        return Response(logarModel);
    }

}