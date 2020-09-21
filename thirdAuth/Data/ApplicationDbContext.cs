using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using thirdAuth.Models;

namespace thirdAuth.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {   
        public DbSet<Student> students { get; set;}
        public DbSet<Teacher> teachers { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
