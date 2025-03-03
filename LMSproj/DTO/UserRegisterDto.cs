using System.ComponentModel.DataAnnotations;

namespace LMSproj.DTO
{
    public class UserRegisterDto
    {
        public string FullName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [MinLength(6)]
        public string Password { get; set; }
    }
}
