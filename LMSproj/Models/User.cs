using System.ComponentModel.DataAnnotations;

namespace LMSproj.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string PasswordHash { get; set; }

        [Required]
        public string Role { get; set; } 

        public ICollection<BookIssue> BookIssues { get; set; }
        public ICollection<BookRequest> BookRequests { get; set; }
        public ICollection<UserActivityLog> ActivityLogs { get; set; }
    }
}
