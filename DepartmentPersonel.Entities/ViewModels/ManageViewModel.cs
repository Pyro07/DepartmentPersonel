using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentPersonel.Entities.ViewModels
{
    public class UserProfileViewModel
    {
        [StringLength(45, ErrorMessage = "Karakter limitini aştınız")]
        [Required(ErrorMessage = "Lütfen adınızı giriniz"), Display(Name = "Adınız")]
        public string Name { get; set; }

        [StringLength(60, ErrorMessage = "Karakter limitini aştınız")]
        [Required(ErrorMessage = "Lütfen soyadınızı giriniz"), Display(Name = "Soyadınız")]
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
    public class ChangePasswordViewModel
    {
        [Required, Display(Name = "Eski Parola"), StringLength(100, MinimumLength = 6, ErrorMessage = "Parolanız en az 6 karakter olmalıdır."),
          DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required, Display(Name = "Yeni Parola"), StringLength(100, MinimumLength = 6, ErrorMessage = "Parolanız en az 6 karakter olmalıdır."),
          DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required, Display(Name = "Yeni Parolanızı Onaylayın"), DataType(DataType.Password),
           Compare("NewPassword", ErrorMessage = "Parolalar birbiri ile uyuşmuyor.")]
        public string ConfirmNewPassword { get; set; }
    }

    public class UserProfilePasswordViewModel
    {
        public UserProfileViewModel UserProfileViewModel { get; set; }
        public ChangePasswordViewModel ChangePasswordViewModel { get; set; }
    }
}
