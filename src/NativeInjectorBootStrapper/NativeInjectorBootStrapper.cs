using AutoMapper;
using Eventos.IO.Application.AUTOMAPPER;
using Eventos.IO.Application.INTERFACES;
using Eventos.IO.Application.SERVICES;
using Eventos.IO.Domain.Core.Bus;
using Eventos.IO.Domain.Core.EVENTS;
using Eventos.IO.Domain.Core.NOTIFICATIONS;
using Eventos.IO.Domain.Eventos.COMMANDHANDLERS;
using Eventos.IO.Domain.Eventos.COMMANDS;
using Eventos.IO.Domain.Eventos.EVENTS;
using Eventos.IO.Domain.Eventos.Repository;
using Eventos.IO.Domain.INTERFACES;
using Eventos.IO.Domain.Organizadores.COMMANDS;
using Eventos.IO.Domain.Organizadores.EVENTS;
using Eventos.IO.Domain.Organizadores.REPOSITORY;
using Eventos.IO.Infra.CrossCuting.Bus;
using Eventos.IO.Infra.CrossCutting.AspNetFilters;
using Eventos.IO.Infra.Data.CONTEXT;
using Eventos.IO.Infra.Data.REPOSITORY;
using Eventos.IO.Infra.Data.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Eventos.IO.Infra.CrossCuting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {

            //ASPNET
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //Application
            services.AddSingleton<IConfigurationProvider>(AutoMapperConfiguration.RegisterMappings());
            services.AddScoped<IMapper>(
                sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));
            services.AddScoped<IEventoAppService, EventoAppService>();
            services.AddScoped<IOrganizadorAppService, OrganizadorAppService>();

            //DOMAIN Commands
            services.AddScoped<IHandler<RegistrarEventoCommand>, EventoCommandHandler>();
            services.AddScoped<IHandler<AtualizarEventoCommand>, EventoCommandHandler>();
            services.AddScoped<IHandler<AtualizarEnderecoEventoCommand>, EventoCommandHandler>();
            services.AddScoped<IHandler<IncluirEnderecoEventoCommand>, EventoCommandHandler>();
            services.AddScoped<IHandler<ExcluirEventoCommand>, EventoCommandHandler>();
            services.AddScoped<IHandler<RegistrarOrganizadorCommand>, OrganizadorCommandHandler>();

            //Domain - Eventos
            services.AddScoped<IDomainNotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<IHandler<EventoRegistradoEvent>, EventoEventHandler>();
            services.AddScoped<IHandler<EnderecoEventoAdicionadoEvent>, EventoEventHandler>();
            services.AddScoped<IHandler<EnderecoEventoAtualizadoEvent>, EventoEventHandler>();
            services.AddScoped<IHandler<EventoAtualizadoEvent>, EventoEventHandler>();
            services.AddScoped<IHandler<EventoExcluidoEvent>, EventoEventHandler>();
            services.AddScoped<IHandler<OrganizadorRegistradoEvent>, OrganizadorEventHandler>();

            //Infra - Data
            services.AddScoped<IEventoRepository, EventoRepository>();
            services.AddScoped<IOrganizadorRepository, OrganizadorRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<EventosContext>();
            //Infra Bus
             services.AddScoped<IBus, InMemoryBus>();

             //infra Filtros
             services.AddScoped<ILogger<GlobalExceptionHandlingFilter>,Logger<GlobalExceptionHandlingFilter>>();
             services.AddScoped<GlobalExceptionHandlingFilter>();

             services.AddScoped<ILogger<GlobalActionLogger>,Logger<GlobalActionLogger>>();
             services.AddScoped<GlobalActionLogger>();

        }
    }
}