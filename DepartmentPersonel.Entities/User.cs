using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentPersonel.Entities
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Email boş geçilemez"), Display(Name = "Email Adresi"), 
            DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Lütfen geçerli bir email adresi giriniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı boş geçilemez"), Display(Name ="Kullanıcı Adı")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Şifre boş geçilemez"), Display(Name ="Şifre"),
            StringLength(15, ErrorMessage = "Şifreniz en az 6 en fazla 15 karakter olabilir",  MinimumLength = 6),
            DataType(DataType.Password)]
        public string UserPassword { get; set; }

        [Required(ErrorMessage = "Şifre doğrulama boş geçilemez"), Display(Name = "Şifrenizi Doğrulayın"),
            StringLength(15, ErrorMessage ="Şifreniz en az 6 en fazla 15 karakter olabilir", MinimumLength = 6),
            DataType(DataType.Password), Compare("UserPassword", ErrorMessage = "Şifreler birbiri ile eşleşmiyor")]
        public string UserPasswordConfirm { get; set; }

        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}
