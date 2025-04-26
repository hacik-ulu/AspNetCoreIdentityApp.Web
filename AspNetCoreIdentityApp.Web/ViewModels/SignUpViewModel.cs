using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.ViewModels
{
    public class SignUpViewModel
    {
        public SignUpViewModel()
        {

        }
        public SignUpViewModel(string userName, string email, string phone, string password)
        {
            UserName = userName;
            Email = email;
            Phone = phone;
            Password = password;
        }

        [Required(ErrorMessage = "Kullanıcı adı boş olamaz!")]
        [Display(Name = "Kullanıcı Adı :")]
        public string UserName { get; set; }

        [EmailAddress(ErrorMessage = "Geçersiz email adresi!")]
        [Required(ErrorMessage = "Email boş olamaz!")]
        [Display(Name = "Email :")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon boş olamaz!")]
        [Display(Name = "Telefon :")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Şifre boş olamaz!")]
        [Display(Name = "Şifre :")]
        public string Password { get; set; }

        [Compare(nameof(Password),ErrorMessage ="Şifreler uyuşmuyor!")]
        [Required(ErrorMessage = "Şifre tekrarı boş olamaz!")]
        [Display(Name = "Şifre Tekrarı :")]
        public string PasswordConfirm { get; set; }
    }
}
