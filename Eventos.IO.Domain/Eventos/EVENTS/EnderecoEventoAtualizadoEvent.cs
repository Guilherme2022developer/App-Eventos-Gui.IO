using System;
using Eventos.IO.Domain.Core.EVENTS;

namespace Eventos.IO.Domain.Eventos.EVENTS;

public class EnderecoEventoAtualizadoEvent : Event
{
    public Guid Id { get; set; }
    public string Logradouro { get; set; }
    public string Numero { get; set; }
    public string Complemento { get; set; }
    public string Bairro { get; set; }
    public string CEP { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }

    public EnderecoEventoAtualizadoEvent(Guid enderecoId, string logradouro, string numero, string complemento, string bairro, string cep, string cidade, string estado, Guid eventoId)
    {
        Id = enderecoId;
        Logradouro = logradouro;
        Numero = numero;
        Complemento = complemento;
        Bairro = bairro;
        CEP = cep;
        Cidade = cidade;
        Estado = estado;
        AggregateId = eventoId;
    }
}