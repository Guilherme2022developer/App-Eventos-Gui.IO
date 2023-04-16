using System.Threading.Tasks.Dataflow;
using Microsoft.AspNetCore.Mvc;
using Eventos.IO.Application.VIEWMODELS;
using Eventos.IO.Application.INTERFACES;
using Eventos.IO.Domain.Core.NOTIFICATIONS;
using Eventos.IO.Domain.Eventos;
using Eventos.IO.Domain.INTERFACES;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Eventos.IO.Site.Controllers
{
    [Route("")]
    public class EventosController : BaseController
    {
        private readonly IEventoAppService _eventoAppService;

        public EventosController(IEventoAppService eventoAppService,
                                 IDomainNotificationHandler<DomainNotification> notification,
                                 IUser user) : base(notification, user)
        {
            _eventoAppService = eventoAppService;
        }

        [Route("")]
        [Route("proximos-eventos")]
        public IActionResult Index()
        {
            return View(_eventoAppService.ObterTodos());
        }

        [Authorize(Policy = "PodeLerEventos")]
        [Route("meus-eventos")]

        public IActionResult MeusEventos()
        {
            return View(_eventoAppService.ObterEventoPorOrganizador(OrganizadorId));
        }


        [Route("dados-do-evento/{id:guid}")]
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventoViewModel = _eventoAppService.ObterEventoPorId(id.Value);
            if (eventoViewModel == null)
            {
                return NotFound();
            }

            return View(eventoViewModel);
        }

        [Authorize(Policy = "PodeGravar")]
        [Authorize]
        [Route("novo-evento")]

        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Policy = "PodeGravar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("novo-evento")]
        public IActionResult Create(EventoViewModel eventoViewModel)
        {

           // ModelState.Remove("Categoria.Nome");
            ModelState.Clear();
            eventoViewModel.CategoriaId = eventoViewModel.CategoriaId;
            eventoViewModel.OrganizadorId = OrganizadorId;
            _eventoAppService.Registrar(eventoViewModel);

            ViewBag.RetornoPost = OperacaoValida() ? "success,Evento registrado com sucesso" : "error,Evento não registrado! verifique as mensagens";

            return View(eventoViewModel);
        }

        [Authorize(Policy = "PodeGravar")]
        [Route("editar-evento/{id:guid}")]

        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventoViewModel = _eventoAppService.ObterEventoPorId(id.Value);

            if (eventoViewModel == null)
            {
                return NotFound();
            }

            if (eventoViewModel.OrganizadorId != OrganizadorId )
            {
                return RedirectToAction("MeusEventos",_eventoAppService.ObterEventoPorOrganizador(OrganizadorId));
            }
                    
            return View(eventoViewModel);
        }

        [Authorize(Policy = "PodeGravar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("editar-evento/{id:guid}")]

        public IActionResult Edit(EventoViewModel eventoViewModel)
        {

            if (eventoViewModel.OrganizadorId != OrganizadorId)
            {
                return RedirectToAction("MeusEventos", _eventoAppService.ObterEventoPorOrganizador(OrganizadorId));
            }

            ModelState.Remove("Categoria.Nome");
            //if (ModelState.IsValid) return View(eventoViewModel);
            ModelState.Clear();
            eventoViewModel.OrganizadorId = OrganizadorId;

            _eventoAppService.Atualizar(eventoViewModel);

            ViewBag.RetornoPost = OperacaoValida() ? "success,Evento atualizado com sucesso" : "error,Evento não pode ser atualizado! verifique as mensagens";

            if (_eventoAppService.ObterEventoPorId(eventoViewModel.Id).Online)
            {
                eventoViewModel.Endereco = null;
            }
            else
            {
                eventoViewModel = _eventoAppService.ObterEventoPorId(eventoViewModel.Id);
            }

            return View(eventoViewModel);
        }

        [Authorize(Policy = "PodeGravar")]
        [Route("excluir-evento/{id:guid}")]
        public IActionResult Delete(Guid? id)
        { 
            if (id == null)
            {
                return NotFound();
            }

            var eventoViewModel = _eventoAppService.ObterEventoPorId(id.Value);

            if (eventoViewModel.OrganizadorId != OrganizadorId)
            {
                return RedirectToAction("MeusEventos", _eventoAppService.ObterEventoPorOrganizador(OrganizadorId));
            }

            if (eventoViewModel == null)
            {
                return NotFound();
            }

            return View(eventoViewModel);
        }

        [Authorize(Policy = "PodeGravar")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("excluir-evento/{id:guid}")]

        public IActionResult DeleteConfirmed(Guid id)
        {
            var eventoViewModel = _eventoAppService.ObterEventoPorId(id);

            if (eventoViewModel.OrganizadorId != OrganizadorId)
            {
                return RedirectToAction("MeusEventos", _eventoAppService.ObterEventoPorOrganizador(OrganizadorId));
            }

            _eventoAppService.Excluir(id);
            return Redirect("Index");
        }

        [Authorize(Policy = "PodeGravar")]
        [Route("incluir-endereco/{id:guid}")]
        public IActionResult IncluirEndereco(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var eventoViewModel = _eventoAppService.ObterEventoPorId(id.Value);

            return PartialView("_IncluirEndereco", eventoViewModel); ;
        }

        [Authorize(Policy = "PodeGravar")]
        [Route("atualizar-endereco/{id:guid}")]
        public IActionResult AtualizarEnderecoObter(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var eventoViewModel = _eventoAppService.ObterEventoPorId(id.Value);
            return PartialView("_AtualizarEndereco",eventoViewModel);
        }

        [Authorize(Policy = "PodeGravar")]
        [HttpPost, ActionName("Atualizar Endereço")]
        [ValidateAntiForgeryToken]
        [Route("atualizar-endereco/{id:guid}")]
        public IActionResult AtualizarEndereco(EventoViewModel eventoViewModel)
        {
            ModelState.Clear();
            _eventoAppService.AtualizarEndereco(eventoViewModel.Endereco);
            if (OperacaoValida())
            {
                //var url = Url.Action("ObterEndereco", "Eventos", new { id = eventoViewModel.Id });
                return Redirect("Index");

            }

            return PartialView("_AtualizarEndereco", eventoViewModel);
        }

        [Authorize(Policy = "PodeGravar")]
        [HttpPost, ActionName("Adicionar Endereço")]
        [ValidateAntiForgeryToken]
        [Route("incluir-endereco/{id:guid}")]
        public IActionResult IncluirEndereco(EventoViewModel eventoViewModel)
        {
            ModelState.Clear();
            eventoViewModel.Endereco.EventoId = eventoViewModel.Id;
            _eventoAppService.AdicionarEndereco(eventoViewModel.Endereco);

            if (OperacaoValida())
            {
                var url = Url.Action("ObterEndereco", "Eventos" ,new { id = eventoViewModel.Id });
                return Json(new { success = true, url = url });
            }

            return PartialView("_IncluirEndereco", eventoViewModel);
        }

        [Route("listar-endereco/{id:guid}")]
        public IActionResult ObterEndereco(Guid Id)
        {

            return PartialView("_DetalhesEndereco", _eventoAppService.ObterEventoPorId(Id));
        }


    }
}
