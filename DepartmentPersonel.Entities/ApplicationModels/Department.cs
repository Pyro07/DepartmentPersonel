using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentPersonel.Entities.ApplicationModels
{
    [Table("Department")]
    public class Department
    {
        public Department()
        {
            Personels = new List<Personel>();
        }
        public int Id { get; set; }
        [Required(ErrorMessage ="Departman adı boş geçilemez"), Display(Name="Departman Adı"), 
            StringLength(60, ErrorMessage = "Lütfen 100 karakterden fazla değer girmeyiniz.")]
        public string Name { get; set; }
        public ICollection<Personel> Personels { get; set; }
    }
}
