using System.ComponentModel.DataAnnotations;
using System;

namespace Eventos.IO.Application.VIEWMODELS
{
    public class OrganizadorViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome é requerido")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "CPF Requerido")]
        [StringLength(11)]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Email requerido")]
        [EmailAddress(ErrorMessage = "Email em formato inválido")]
        public string Email { get; set; }
    }
}