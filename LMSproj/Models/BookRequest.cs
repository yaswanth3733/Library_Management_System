using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LMSproj.Models
{
    public class BookRequest
    {
        [Key]
        public int RequestId { get; set; }

        [Required]
        public int UserId { get; set; } 

        [ForeignKey("UserId")]
        public User User { get; set; }  

        [Required]
        public int BookId { get; set; }  

        [ForeignKey("BookId")]
        public Book Book { get; set; }  

        [Required]
        public DateTime RequestDate { get; set; }

        [Required]
        public string Status { get; set; } 
    }
}
