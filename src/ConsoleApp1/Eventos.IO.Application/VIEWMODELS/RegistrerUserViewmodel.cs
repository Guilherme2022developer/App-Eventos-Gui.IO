using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Validators;

namespace Eventos.IO.Application.VIEWMODELS;

public class RegistrerUserViewmodel 
{
    [Required(ErrorMessage = "O campo {0}´é obrigatatório")]
    [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo {0}´é obrigatatório")]
    [StringLength(100,ErrorMessage = "O campo {0} precisa ter entre {2} e ,mi{1} caracteres", MinimumLength = 6)]
    public string Password { get; set; }


    [Compare("Password",ErrorMessage = "As senhas não conferem")]
    public string ConfirmPassWord { get; set; }

    public string? Token { get; set; }

    public string? role { get; set; }




}

public class LoginUserViewModel
{
     public Guid? Id { get; set; }

    [Required(ErrorMessage = "O campo {0}´é obrigatatório")]
    [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
    public string Email { get; set; }


    [Required(ErrorMessage = "O campo {0}´é obrigatatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e ,mi{1} caracteres", MinimumLength = 6)]
    public string Password { get; set; }

    public string? Token { get; set; }

    public string? role { get; set; }

}