namespace LMSproj.DTO
{
    public class FineDto
    {
        public int FineId { get; set; }

        public int IssueId { get; set; }

        public decimal FineAmount { get; set; }

        public bool IsPaid { get; set; }

        public DateTime? PaymentDate { get; set; } // Nullable if not paid
    }
}
