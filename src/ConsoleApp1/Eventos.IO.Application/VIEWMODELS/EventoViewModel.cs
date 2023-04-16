using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Eventos.IO.Application.VIEWMODELS
{
    public class EventoViewModel
    {
        public EventoViewModel()
        {
            Id = Guid.NewGuid();
            Endereco = new EnderecoViewModel();
            Categoria = new CategoriaViewModel();
        }

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Nome é requerido")]
        [MinLength(2, ErrorMessage = "Tamanho minimo requerido 2")]
        [MaxLength(150, ErrorMessage = "Tamanho maximo 150 caracteres")]
        [Display(Name = "Nome do Evento")]
        public string Nome { get; set; }

        [Display(Name = "Descrição curta do evento")]
        [Required(ErrorMessage = "A descrição curta é requerida")]
        public string DescricaoCurta { get; set; }

        [Display(Name = "Descrição longa do evento")]
        [Required(ErrorMessage = "A descrição longa é requerida")]
        public string DescricaoLonga { get; set; }

        [Display(Name = "Inicio do evento")]
        [Required(ErrorMessage = "A data é requerida")]
        public DateTime DataInicio { get; set; }

        [Display(Name = "Fim do evento")]
        [Required(ErrorMessage = "A data é requerida")]
        public DateTime DataFim { get; set; }

        [Display(Name = "Será gratuito?")]
        public bool Gratuito { get; set; }

        [Display(Name = "Valor")]
        [Required(ErrorMessage = "O Valor é requerido")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [DataType(DataType.Currency, ErrorMessage = "Moeda em formato inválido")]
        public decimal Valor { get; set; }

        [Display(Name = "Será Online?")]
        public bool Online { get; set; }

        [Display(Name = "Empresa / Grupo Organizador")]
        [Required(ErrorMessage = "A Empresa / Grupo Organizador é requerida")]

        public string NomeEmpresa { get; set; }

        public EnderecoViewModel Endereco { get; set; }

        public CategoriaViewModel Categoria { get; set; }

        public Guid CategoriaId { get; set; }
        public Guid OrganizadorId { get; set; }

        public Guid? EnderecoId { get; set; }
    }
}
