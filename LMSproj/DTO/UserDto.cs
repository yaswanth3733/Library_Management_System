﻿namespace LMSproj.DTO
{
    public class UserDto
    {
        public int UserId { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Role { get; set; } // "Admin" or "User"
    }
}
