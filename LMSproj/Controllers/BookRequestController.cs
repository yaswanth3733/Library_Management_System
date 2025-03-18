using LMSproj.DTO;
using LMSproj.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace LMSproj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookRequestController : ControllerBase

    {
        
        private readonly LibraryContext _context;

        public BookRequestController(LibraryContext context)
        {
            _context = context;
        }



        // ✅ User requests a book
        [HttpPost("request")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<BookRequestDto>> RequestBook([FromBody] AddBookRequestDto requestDto)

        {

            if (!ModelState.IsValid)

                return BadRequest(ModelState);



            var user = await _context.Users.FindAsync(requestDto.UserId);

            var book = await _context.Books.FindAsync(requestDto.BookId);



            if (user == null || book == null)

                return NotFound("User or Book not found.");



            if (book.AvailableCopies <= 0)

                return BadRequest("No available copies of this book.");

            // Check if the user has any overdue books
            bool hasOverdueBooks = await _context.BookIssues
                .AnyAsync(b => b.UserId == requestDto.UserId && b.ReturnDate == null && b.DueDate < DateTime.UtcNow);
            if (hasOverdueBooks)
                return BadRequest("You have overdue books. Please return them before requesting new ones.");

            var bookRequest = new BookRequest

            {

                UserId = requestDto.UserId,

                BookId = requestDto.BookId,

                Status = "Pending"

            };



            _context.BookRequests.Add(bookRequest);

            await _context.SaveChangesAsync();



            return Ok(new BookRequestDto

            {

                RequestId = bookRequest.RequestId,

                UserId = bookRequest.UserId,

                BookId = bookRequest.BookId,

                Status = bookRequest.Status

            });

        }



        // ✅ Get all book requests

        [HttpGet("all")]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<IEnumerable<BookRequestDto>>> GetAllRequests()

        {

            var requests = await _context.BookRequests
                .Include(r => r.Book)
                .Include(r => r.User)
                .ToListAsync();



            var requestDtos = requests.Select(r => new BookRequestDto

            {

                RequestId = r.RequestId,

                UserId = r.UserId,

                UserName = r.User.FullName,

                BookId = r.BookId,

                Title = r.Book.Title,

                Status = r.Status

            }).ToList();



            return Ok(requestDtos);

        }

        [HttpGet("user/{userId}")]

        [Authorize]

        public async Task<ActionResult<IEnumerable<BookRequestDto>>> GetUserBookRequests(int userId)

        {

            var userRequests = await _context.BookRequests

                .Where(r => r.UserId == userId)

                .Include(r => r.Book)

                .Select(r => new BookRequestDto

                {

                    RequestId = r.RequestId,

                    Title = r.Book != null ? r.Book.Title : "UnKnown",  // ✅ Include book title

                    Status = r.Status  // "Pending", "Accepted", "Rejected"

                })

                .ToListAsync();

            if (!userRequests.Any())

                return NotFound("No book requests found for this user.");

            return Ok(userRequests);

        }




        [HttpPut("update/{requestId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRequestStatus(int requestId, [FromBody] UpdateStatusDto statusDto)
        {
            var request = await _context.BookRequests.FindAsync(requestId);
            if (request == null)
                return NotFound("Request not found.");

            var book = await _context.Books.FindAsync(request.BookId);
            if (book == null)
                return NotFound("Book not found.");

            if (statusDto.Status == "Accepted" && book.AvailableCopies > 0)
            {
                request.Status = "Accepted";
                book.AvailableCopies -= 1; // Reduce available copies

                _context.BookIssues.Add(new BookIssue
                {
                    UserId = request.UserId,
                    BookId = request.BookId,
                    IssueDate = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(14) // Example: 14-day loan period
                });

                var user = await _context.Users.FindAsync(request.UserId);
                _context.UserActivityLogs.Add(new UserActivityLog
                {
                    Username = user.FullName,
                    BookName = book.Title,
                    Action = "Borrowed",
                    Description = $"Book '{book.Title}' was borrowed by '{user.FullName}'.",
                    Timestamp = DateTime.Now
                });
            }
            else
            {
                request.Status = "Rejected";
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        
        [HttpDelete("delete/{requestId}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteRequest(int requestId)
        {
            var request = await _context.BookRequests.FindAsync(requestId);
            if (request == null)
                return NotFound("Request not found.");
            if (request.Status == "Accepted" || request.Status == "Rejected")
            {
                return BadRequest("Cannot delete a book request that has been accepted or rejected.");
            }
            _context.BookRequests.Remove(request);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
