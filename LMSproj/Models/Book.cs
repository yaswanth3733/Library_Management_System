using System.ComponentModel.DataAnnotations;

namespace LMSproj.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        [MaxLength(100)]
        public string Author { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public int TotalCopies { get; set; }

        public int AvailableCopies { get; set; }

        [Required]
        public string Category { get; set; } // Directly storing category as string

        public ICollection<BookIssue> BookIssues { get; set; }
        public ICollection<BookRequest> BookRequests { get; set; }

    }
}
