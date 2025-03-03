namespace LMSproj.DTO
{
    public class BookIssueDto
    {
        public int IssueId { get; set; }

        public int UserId { get; set; }

        public int BookId { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? ReturnDate { get; set; } // Nullable if not returned

        public decimal FineAmount { get; set; }
    }
}
