using Eventos.IO.Domain.Core.Bus;
using Eventos.IO.Domain.Core.NOTIFICATIONS;
using Eventos.IO.Domain.INTERFACES;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Eventos.IO.Services.Api.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public abstract class BaseController : Controller
    {
        private readonly IDomainNotificationHandler<DomainNotification> _domainNotification;
        private readonly IBus _bus;

        public Guid OrganizadorId { get; set; }      
        protected BaseController(IDomainNotificationHandler<DomainNotification> domainNotification, IUser user, IBus bus)
        {
            _domainNotification = domainNotification;
            _bus = bus;

            if (user.IsAuthenticated())
            {
                OrganizadorId = user.GetUserId();
            }
        }

        protected new IActionResult Response(object result = null)
        {
            if (OperacaoValida())
            {
                return Ok(new
                {
                    succes = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _domainNotification.GetNotifications().Select(n => n.Value)
            });
        }
        protected bool OperacaoValida()
        {
            return (!_domainNotification.HasNotifications());
        }

        protected void NotificarErroModelInvalida()
        {
            var erros = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(string.Empty, erroMsg);
            }
        }

        protected void NotificarErro(string codigo, string mensagem)
        {
            _bus.RaiseEvent(new DomainNotification(codigo, mensagem));
        }

        protected void AdicionarErrosIdentity(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                NotificarErro(result.ToString(), error.Description);
            }
        }

    }
}
