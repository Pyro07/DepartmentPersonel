using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentPersonel.Entities.ApplicationModels
{
    [Table("Personel")]
    public class Personel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Personel kimlik numarası boş geçilemez"), Display(Name = "Personek Kimlik Numarası")]
        public long IdentityNumber { get; set; }

        [Required(ErrorMessage ="Personel adı boş geçilemez"), Display(Name="Personel Adı"), 
            StringLength(50, ErrorMessage ="Lütfen 50 karakterden fazla değer girmeyiniz")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Personel soyadı boş geçilemez"), Display(Name="Personel Soyadı"), 
            StringLength(70, ErrorMessage ="Lütfen 70 karakterden fazla değer girmeyiniz")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Personel maaşı boş geçilemez"), Display(Name="Personel Maaşı"),
            Range(1,100000, ErrorMessage ="Personel maaşı en az 1 en fazla 100.000 olabilir")]
        public decimal PersonelSalary { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

    }
}
