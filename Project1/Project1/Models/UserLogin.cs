using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Email address is required for login")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Password is required for login")]
        public string UserPassword { get; set; }
    }
}
