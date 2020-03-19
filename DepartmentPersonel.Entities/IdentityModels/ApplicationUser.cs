using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DepartmentPersonel.Entities.IdentityModels
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }

        [StringLength(50, ErrorMessage = "Karakter limitini aştınız")]
        [Required(ErrorMessage = "Ad kısmı boş geçilemez"), Display(Name = "Adınız")]
        public string Name { get; set; }

        [StringLength(60, ErrorMessage = "Karakter limitini aştınız")]
        [Required(ErrorMessage = "Soyad kısmı boş geçilemez"), Display(Name = "Soyadınız")]
        public string LastName { get; set; }
    }
}
