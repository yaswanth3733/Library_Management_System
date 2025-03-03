using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LMSproj.Models
{
    public class Fine
    {
        [Key]
        public int FineId { get; set; }

        [Required]
        public int IssueId { get; set; }  

        [ForeignKey("IssueId")]
        public BookIssue BookIssue { get; set; }  

        [Required]
        public decimal FineAmount { get; set; }

        [Required]
        public bool IsPaid { get; set; } 

        public DateTime? PaymentDate { get; set; } 
    }
}
