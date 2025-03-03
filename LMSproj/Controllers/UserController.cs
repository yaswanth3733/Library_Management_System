using LMSproj.DTO;
using LMSproj.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
namespace LMSproj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly LibraryContext _context;
        private readonly IConfiguration _configuration;
        

        public UserController(LibraryContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        // ✅ User Registration
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterUserDto registerDto)
        {
            try
            {

                if (!ModelState.IsValid) return BadRequest(ModelState);

                if (registerDto.Role != "User" && registerDto.Role != "Admin")
                    return BadRequest("Invalid role. Allowed values: 'User' or 'Admin'.");

                if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
                    return BadRequest("Email is already in use.");

                var newUser = new User
                {
                    FullName = registerDto.FullName,
                    Email = registerDto.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password), 
                    Role = registerDto.Role 
                };

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetUserById), new { id = newUser.UserId }, new UserDto
                {
                    UserId = newUser.UserId,
                    FullName = newUser.FullName,
                    Email = newUser.Email,
                    Role = newUser.Role
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        // ✅ User Login (JWT Authentication)
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
                if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
                    return Unauthorized("Invalid email or password.");

                var token = GenerateJwtToken(user);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");

            }
        }
        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            try
            {
                var users = await _context.Users.ToListAsync();
                var userDtos = users.Select(user => new UserDto
                {
                    UserId = user.UserId,
                    FullName = user.FullName,
                    Email = user.Email,
                    Role = user.Role
                }).ToList();

                return Ok(userDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        // ✅ Retrieve User by ID
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null) return NotFound("User not found");

                return Ok(new UserDto
                {
                    UserId = user.UserId,
                    FullName = user.FullName,
                    Email = user.Email,
                    Role = user.Role
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");

            }
        }
        // ✅ Update User Details
        [HttpPut("update/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] RegisterUserDto updateDto)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null) return NotFound("User not found");

                // Prevent updating email to an already existing email
                if (user.Email != updateDto.Email && await _context.Users.AnyAsync(u => u.Email == updateDto.Email))
                    return BadRequest("Email is already in use.");

                user.FullName = updateDto.FullName;
                user.Email = updateDto.Email;
                if (!string.IsNullOrWhiteSpace(updateDto.Password))
                    user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(updateDto.Password);

                await _context.SaveChangesAsync();
                return Ok("User details updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");

            }
        }

    }
}