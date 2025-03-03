namespace LMSproj.DTO
{
    public class AddBookIssueDto
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}
