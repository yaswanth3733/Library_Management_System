namespace LMSproj.DTO
{
    public class BookDto
    {
        public int BookId { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string ISBN { get; set; }

        public int TotalCopies { get; set; }

        public int AvailableCopies { get; set; }

        public string Category { get; set; }
    }

    public class AddBookDto
         
    {

        public string Title { get; set; }

        public string Author { get; set; }

        public string ISBN { get; set; }

        public int TotalCopies { get; set; }

        public string Category { get; set; }

    }



    public class UpdateBookDto

    {

        public string Title { get; set; }

        public string Author { get; set; }

        public string ISBN { get; set; }

        public int TotalCopies { get; set; }

        public int AvailableCopies { get; set; }

        public string Category { get; set; }

    }
}
