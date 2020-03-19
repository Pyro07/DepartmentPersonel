using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentPersonel.Entities.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Lütfen kullanıcı adınızı giriniz."), Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Lütfen parolanızı giriniz"), Display(Name = "Parola"),
            DataType(DataType.Password), StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }
        [Display(Name = "Beni Hatırla")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [StringLength(45, ErrorMessage = "Karakter limitini aştınız")]
        [Required(ErrorMessage = "Lütfen adınızı giriniz"), Display(Name = "Adınız")]
        public string Name { get; set; }

        [StringLength(60, ErrorMessage = "Karakter limitini aştınız")]
        [Required(ErrorMessage = "Lütfen soyadınızı giriniz"), Display(Name = "Soyadınız")]
        public string LastName { get; set; }

        [StringLength(45, ErrorMessage = "Karakter limitini aştınız")]
        [Required(ErrorMessage = "Lütfen bir kullanıcı adı giriniz"), Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Lütfen e-posta adresi giriniz"), Display(Name = "E-posta")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Lütfen geçerli bir e-posta adresi giriniz")]
        public string EmailAddress { get; set; }

        [StringLength(16, ErrorMessage = "Parolanız en az 6 en fazla 16 karakter olabilir", MinimumLength = 6)]
        [Required(ErrorMessage = "Lütfen bir parola giriniz"), Display(Name = "Parola")]
        [DataType(DataType.Password, ErrorMessage = "Lütfen geçerli bir parola giriniz")]
        public string Password { get; set; }

        [StringLength(16, ErrorMessage = "Parolanız en az 6 en fazla 16 karakter olabilir", MinimumLength = 6)]
        [Compare("Password", ErrorMessage = "Parolalar birbiri ile eşleşmiyor")]
        [Required(ErrorMessage = "Lütfen bir onay parola giriniz"), Display(Name = "Onay Parolası")]
        [DataType(DataType.Password, ErrorMessage = "Lütfen geçerli bir parola giriniz")]
        public string ConfirmPassword { get; set; }
    }

    public class SendEmailViewModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string ActivationCode { get; set; }
        public string ResetPasswordToken { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required, Display(Name = "Email adresi"),
            DataType(DataType.EmailAddress, ErrorMessage = "Lütfen geçerli bir email adresi giriniz")]
        public string Email { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required, Display(Name = "E-posta adresi"), DataType(DataType.EmailAddress, ErrorMessage = "Lütfen geçerli bir e-posta adresi giriniz")]
        public string Email { get; set; }
        [Required, Display(Name = "Parola"), DataType(DataType.Password, ErrorMessage = "Lütfen geçerli bir parola giriniz"),
            StringLength(16, ErrorMessage = "Parolanız en az 6 en fazla 16 karakter olabilir", MinimumLength = 6)]
        public string Password { get; set; }
        [Required, Display(Name = "Parolanızı onaylayın"), DataType(DataType.Password, ErrorMessage = "Lütfen geçerli bir parola giriniz"),
            StringLength(16, ErrorMessage = "Parolanız en az 6 en fazla 16 karakter olabilir", MinimumLength = 6),
            Compare("Password", ErrorMessage = "Parolalar birbiri ile eşleşmiyor")]
        public string ConfirmPasword { get; set; }
        public string Code { get; set; }
    }
}
