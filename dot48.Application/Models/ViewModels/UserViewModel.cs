using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dot48.Application.Models.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            Profiles = new List<ProfileViewModel>();
        }
        public int IdUser { get; set; }

        [Required(ErrorMessage = "Entra Nome", AllowEmptyStrings = false)]
        [StringLength(200, MinimumLength = 4)]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Entra e-mail", AllowEmptyStrings = false)]
        [StringLength(200, MinimumLength = 4)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Entra Senha", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [StringLength(200, MinimumLength = 8)]
        public string Password { get; set; }

        public bool Enable { get; set; }
        public int IdProfile { get; set; }
        public List<ProfileViewModel> Profiles { get; set; }

        [StringLength(150)]
        [DataType(DataType.Text)]
        public string CodeUser { get; set; }

        [StringLength(150)]
        [DataType(DataType.Text)]
        public string AliasName { get; set; }

        [StringLength(20)]
        [DataType(DataType.Text)]
        public string Nif { get; set; }

        [StringLength(14)]
        public string PhoneNumber { get; set; }
        public int IdUserSetting { get; set; }
        public bool MO { get; set; }
        public bool RecipientMO { get; set; }
        public bool DIR { get; set; }
        public bool RecipientDIR { get; set; }
        public short WorkHours { get; set; }
    }
}