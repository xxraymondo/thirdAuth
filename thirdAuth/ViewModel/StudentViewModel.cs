﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace thirdAuth.ViewModel
{
    public class StudentViewModel
    {
       
        public string? UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "password doesn't match")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string year { get; set; }
    }
}
