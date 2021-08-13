using Project1.Business.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace Project1.Business
{
    public partial class UserModel
    {
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public bool UserIsAdmin { get; set; }
        public List<ClassModel> Classes { get; set; }
    }
}
