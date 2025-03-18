namespace LMSproj.DTO
{
    
    public class AddBookIssueDto

    {

        public int UserId { get; set; }

        public int BookId { get; set; }

        public DateTime? ReturnDate { get; set; }

    }


    public class BookIssueDto

    {

        public int IssueId { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public int BookId { get; set; }

        public string Title { get; set; }

        public DateTime IssueDate { get; set; }  // ✅ Borrowed Date

        public DateTime DueDate { get; set; }  // ✅ Returned Date

        public DateTime? ReturnDate { get; set; }  // ✅ Returned Date
        public decimal FineAmount { get; set; }  // ✅ Fine Paid

        public string Status { get; set; }


    }

    // DTO for returning book issue details

    



    // DTO for returning a book

    public class ReturnBookDto

    {

        public int IssueId { get; set; }
        public DateTime ReturnDate { get; set; }

    }


}
