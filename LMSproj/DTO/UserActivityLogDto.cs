namespace LMSproj.DTO
{
    public class UserActivityLogDto
    {
        public int ActivityId { get; set; }

        public int UserId { get; set; }

        public string Action { get; set; } // "Login", "Logout", "Book Requested", etc.

        public DateTime Timestamp { get; set; }

        public string Description { get; set; }
    }
}
