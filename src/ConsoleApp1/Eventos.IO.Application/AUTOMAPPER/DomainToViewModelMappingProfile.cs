using AutoMapper;
using Eventos.IO.Application.VIEWMODELS;
using Eventos.IO.Domain.Eventos;
using Eventos.IO.Domain.Organizadores;

namespace Eventos.IO.Application.AUTOMAPPER
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Evento, EventoViewModel>();
            CreateMap<Endereco, EnderecoViewModel>();
            CreateMap<Categoria, CategoriaViewModel>();
            CreateMap<Organizador, OrganizadorViewModel>();
        }
    }
}