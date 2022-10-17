using System.ComponentModel.DataAnnotations;

namespace StudentsApi.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Informe o e-mail")]
    [EmailAddress(ErrorMessage = "Formato do e-mail inválido")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Informe a senha")]
    [MinLength(8, ErrorMessage = " A {0} deve ter no mínimo {1} caracteres")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Cofirmar senha")]
    [Compare(nameof(Password), ErrorMessage = "As senha não são compativeis")]
    public string? ConfirmPassword { get; set; }
}
