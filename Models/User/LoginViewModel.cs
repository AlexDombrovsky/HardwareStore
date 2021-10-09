using System.ComponentModel.DataAnnotations;

namespace HardwareStore.Models.User
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введите e-mail")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня")] 
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}