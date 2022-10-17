using System.ComponentModel.DataAnnotations;

namespace StudentsApi.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Informe o e-mail")]
    [EmailAddress(ErrorMessage = "Formato de e-mail inválido")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Informe a senha")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
}
