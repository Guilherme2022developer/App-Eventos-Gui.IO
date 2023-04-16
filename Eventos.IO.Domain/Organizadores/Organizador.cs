using Eventos.IO.Domain.Core.Models;
using Eventos.IO.Domain.Eventos;
using System.Collections.Generic;
using System;
using FluentValidation;

namespace Eventos.IO.Domain.Organizadores
{
    public class Organizador : Entity<Organizador>
    {
        public string Nome { get; private set; }
        public string CPF { get; private set; }
        public string Email { get; private set; }

        public Organizador(Guid id, string nome, string cpf, string email)
        {
            Id = id;
            Nome = nome;
            CPF = cpf;
            Email = email;
        }

        //EF construtor
        protected Organizador() { }

        //EF propriedade de navegação
        public virtual ICollection<Evento> Eventos { get; set; }

        public override bool EhValido()
        {
            Validar();
            return ValidationResult.IsValid;
        }


        private void Validar()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O Nome precisa ser fornecido")
                .Length(2, 100).WithMessage("O  precisa ter entre 2 e 100 caractes");

            RuleFor(c => c.CPF)
                .NotEmpty().WithMessage("O Nome CPF ser fornecido")
                .Length(11, 11).WithMessage("O CPF precisa ter entre 11 caractes");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("O Nome Email ser fornecido");
        }
    }
}