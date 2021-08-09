using System;
using System.Collections.Generic;

#nullable disable

namespace Project1.Repository.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public bool UserIsAdmin { get; set; }
    }
}
