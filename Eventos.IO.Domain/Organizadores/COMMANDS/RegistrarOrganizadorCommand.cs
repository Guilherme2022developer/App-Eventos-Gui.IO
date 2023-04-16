using Eventos.IO.Domain.Core.COMMANDS;
using System;

namespace Eventos.IO.Domain.Organizadores.COMMANDS;

public class RegistrarOrganizadorCommand : Command
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string CPF { get; set; }
    public string Email { get; set; }

    public RegistrarOrganizadorCommand(Guid id, string nome, string cPF, string email)
    {
        Id = id;
        Nome = nome;
        CPF = cPF;
        Email = email;
    }
}