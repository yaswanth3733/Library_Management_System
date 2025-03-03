using LMSproj.DTO;
using LMSproj.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace LMSproj.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserActivityLogController : ControllerBase
    {
        private readonly LibraryContext _context;

        private readonly ILogger<UserActivityLogController> _logger;


        public UserActivityLogController(LibraryContext context, ILogger<UserActivityLogController> logger)
        {
            _logger = logger;
            _context = context;

        }


        // ✅ Get All Logs

        [HttpGet("all")]

        public async Task<ActionResult<IEnumerable<UserActivityLog>>> GetAllLogs()

        {
            try
            {

                var logs = await _context.UserActivityLogs

                    .OrderByDescending(log => log.Timestamp)

                    .ToListAsync();
                _logger.LogInformation("Retrieved all user activity logs successfully.");

                return Ok(logs);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user activity logs.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving user activity logs.");
            }
        }

        [HttpGet("username/{username}")]
        public async Task<ActionResult<IEnumerable<UserActivityLog>>> GetLogsByUsername(string username)
        {
            try
            {
                var logs = await _context.UserActivityLogs
                    .Where(log => log.Username == username)
                    .OrderByDescending(log => log.Timestamp)
                    .ToListAsync();

                if (logs == null || !logs.Any())
                {
                    _logger.LogWarning($"No logs found for user with username '{username}'.");

                    return NotFound($"No logs found for user with username '{username}'");
                }
                _logger.LogInformation($"Retrieved logs for user with username '{username}' successfully.");

                return Ok(logs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving logs for user with username '{username}'.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
            }
        }

        // ✅ Get Borrowed Books
        [HttpGet("borrowed")]
        public async Task<ActionResult<IEnumerable<UserActivityLog>>> GetBorrowedBooks()
        {
            try
            {
                var logs = await _context.UserActivityLogs
                    .Where(log => log.Action == "Borrowed")
                    .OrderByDescending(log => log.Timestamp)
                    .ToListAsync();

                if (logs == null || !logs.Any())
                {
                    _logger.LogWarning("No borrowed books found.");

                    return NotFound("No borrowed books found.");
                }
                _logger.LogInformation("Retrieved all borrowed books successfully.");

                return Ok(logs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving borrowed books.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        // ✅ Get Returned Books
        [HttpGet("returned")]
        public async Task<ActionResult<IEnumerable<UserActivityLog>>> GetReturnedBooks()
        {
            try
            {
                var logs = await _context.UserActivityLogs
                    .Where(log => log.Action == "Returned")
                    .OrderByDescending(log => log.Timestamp)
                    .ToListAsync();

                if (logs == null || !logs.Any())
                {
                    _logger.LogWarning("No returned books found.");

                    return NotFound("No returned books found.");
                }
                _logger.LogInformation("Retrieved all returned books successfully.");

                return Ok(logs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving returned books.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }

        }
    }
}

