using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookHive.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Column(TypeName = "nvarchar(150)")]
        public string fullName { get; set; }
        public string schoolCode { get; set; }
        public bool is_active { get; set; }
        public string filename { get; set; }
    }
}
