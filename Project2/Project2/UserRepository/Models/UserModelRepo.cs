using System;

namespace UserRepository.Models
{
    public class UserModelRepo
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
