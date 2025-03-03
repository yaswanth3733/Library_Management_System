using System.Runtime.CompilerServices;

namespace LMSproj.DTO
{
   
        public class RegisterUserDto

        {

            public string FullName { get; set; }

            public string Email { get; set; }

            public string Password { get; set; }

            public string Role { get; set; }  // "Admin" or "User"

        }



        // ✅ Used for Logging in a User

        public class LoginDto

        {

            public string Email { get; set; }

            public string Password { get; set; }

        }



        // ✅ Used for Retrieving User Details

        public class UserDto

        {

            public int UserId { get; set; }

            public string FullName { get; set; }

            public string Email { get; set; }

            public string Role { get; set; }  // "Admin" or "User"

        }
        public class UpdateUserDto
        {
            public string FullName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
