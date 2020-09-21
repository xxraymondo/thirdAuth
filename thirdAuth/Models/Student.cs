using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace thirdAuth.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string year { get; set; }
        public virtual IdentityUser AppUser { get; set; }
        public string  AppUserId { get; set; }
    }
}
