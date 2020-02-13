using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentPersonel.Entities
{
    public class Personel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Personel adı boş geçilemez"), Display(Name="Personel Adı"), 
            StringLength(50, ErrorMessage ="Lütfen 50 karakterden fazla değer girmeyiniz")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Personel soyadı boş geçilemez"), Display(Name="Personel Soyadı"), 
            StringLength(70, ErrorMessage ="Lütfen 70 karakterden fazla değer girmeyiniz")]
        public string LastName { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
