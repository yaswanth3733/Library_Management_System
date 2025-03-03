namespace LMSproj.DTO
{
   
    public class FineDto

    {

        public int IssueId { get; set; }

        public int UserId { get; set; }

        public int BookId { get; set; }

        public decimal FineAmount { get; set; }

        public bool IsPaid { get; set; }

    }
    public class UserUnpaidFineDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public decimal TotalUnpaidFine { get; set; }
    }
}
