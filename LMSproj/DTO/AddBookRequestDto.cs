namespace LMSproj.DTO
{
        public class AddBookRequestDto

        {

            public int UserId { get; set; }

            public int BookId { get; set; }

        }



        // DTO to return book request details

        public class BookRequestDto

        {

            public int RequestId { get; set; }

            public int UserId { get; set; }

            public string UserName { get; set; }

            public int BookId { get; set; }

            public string Title { get; set; }

          

            public string Status { get; set; }  // "Pending", "Accepted", "Rejected"

        }



        // DTO for Admin to update request status

        public class UpdateStatusDto

        {

            public string Status { get; set; }  // "Accepted" or "Rejected"

        }
    }

