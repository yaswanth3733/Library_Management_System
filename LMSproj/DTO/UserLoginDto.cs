using System.ComponentModel.DataAnnotations;

namespace LMSproj.DTO
{
    public class UserLoginDto
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
