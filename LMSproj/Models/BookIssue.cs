using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.Identity.Client;

namespace LMSproj.Models
{
    public class BookIssue
    {
        [Key]
        public int IssueId { get; set; }

        [Required]
        public int UserId { get; set; }  

        [ForeignKey("UserId")]
        public User User { get; set; }  

        [Required]
        public int BookId { get; set; }  

        [ForeignKey("BookId")]
        public Book Book { get; set; }  

        [Required]
        public DateTime IssueDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public DateTime? ReturnDate { get; set; } 

        public decimal FineAmount { get; set; } 
        [Required]
        public bool IsFinePaid { get; set; } = false;


        public Fine Fine { get; set; } 
    }
}
