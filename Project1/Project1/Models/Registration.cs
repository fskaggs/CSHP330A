using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Project1.Models
{
    public class Registration
    {
        [Required(ErrorMessage = "Email address is required for registration")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is requried for registration")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password is requried for registration")]
        [Compare(nameof(Password))]
        public string Password2 { get; set; }

    }
}
