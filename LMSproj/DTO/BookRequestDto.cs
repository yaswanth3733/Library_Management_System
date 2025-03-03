namespace LMSproj.DTO
{
    public class BookRequestDto
    {
        public int RequestId { get; set; }

        public int UserId { get; set; }

        public int BookId { get; set; }

        public DateTime RequestDate { get; set; }

        public string Status { get; set; } // "Pending", "Approved", "Rejected"
    }
}
