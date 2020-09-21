using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace thirdAuth.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public double Salary { get; set; }
        public virtual IdentityUser appUser { get; set; }
        public string appUserId { get; set; }

    }
}
