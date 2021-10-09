using System.ComponentModel.DataAnnotations;

namespace HardwareStore.Models.User
{
    public class UserRegistrationViewModel
    {
        [EmailAddress(ErrorMessage = "Поле e-mail не является действительным адресом электронной почты.")]
        [Required(ErrorMessage = "Введите e-mail")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Неверный пароль")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [Display(Name = "Подтвердите пароль")]
        public string PasswordConfirm { get; set; }
    }
}