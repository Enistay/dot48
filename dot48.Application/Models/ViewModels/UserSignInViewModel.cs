using System.ComponentModel.DataAnnotations;
namespace dot48.Application.Models.ViewModels
{

    public class UserSignInViewModel
    {
        [Required(ErrorMessage = "Enter E-mail/Código Operador", AllowEmptyStrings= false)]
        [Display(Name = "E-mail")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Enter Password between 3 and 50 caracters", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Password")]
        public string Password { get; set; }

    }
}