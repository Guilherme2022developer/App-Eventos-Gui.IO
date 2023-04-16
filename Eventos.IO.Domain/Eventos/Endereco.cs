using Eventos.IO.Domain.Core.Models;
using System;
using FluentValidation;

namespace Eventos.IO.Domain.Eventos
{
    public class Endereco : Entity<Endereco>
    {
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string CEP { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public Guid? EventoId { get; private set; }

        public Endereco(Guid id, string logradouro, string numero, string complemento, string bairro, string cep, string cidade, string estado, Guid eventoId)
        {
            Id = id;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            CEP = cep;
            Cidade = cidade;
            Estado = estado;
            EventoId = eventoId;
        }

        //EF 
        public virtual Evento evento { get; private set; }

        protected Endereco() { }

        public override bool EhValido()
        {
            RuleFor(c => c.Logradouro)
                .NotEmpty().WithMessage("O Logradouro precisa ser fornecido")
                .Length(2, 150).WithMessage("Necessário entre 2 e 150 caracteres");

            RuleFor(c => c.Bairro)
                .NotEmpty().WithMessage("O Bairro precisa ser fornecido")
                .Length(2, 150).WithMessage("Necessário entre 2 e 150 caracteres");

            RuleFor(c => c.CEP)
                .NotEmpty().WithMessage("CEP precisa ser fornecido")
                .Length(8).WithMessage("Necessário 8 caracteres");

            RuleFor(c => c.Cidade)
                .NotEmpty().WithMessage("Cidade precisa ser fornecida")
                .Length(2, 150).WithMessage("Necessário entre 2 e 150 caracteres");

            RuleFor(c => c.Estado)
                .NotEmpty().WithMessage("Estado precisa ser fornecido")
                .Length(2, 150).WithMessage("Necessário entre 2 e 150 caracteres");

            RuleFor(c => c.Numero)
                .NotEmpty().WithMessage("Numero precisa ser fornecido")
                .Length(1, 10).WithMessage("Necessário entre 1 e 10caracteres");

            ValidationResult = Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
