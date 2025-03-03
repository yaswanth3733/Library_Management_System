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

            var requests = await _context.BookRequests.ToListAsync();



            var requestDtos = requests.Select(r => new BookRequestDto

            {

                RequestId = r.RequestId,

                UserId = r.UserId,

                BookId = r.BookId,

                Status = r.Status

            }).ToList();



            return Ok(requestDtos);

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

        // ✅ Delete a request (Admin only)

        [HttpDelete("delete/{requestId}")]
        [Authorize(Roles ="User")]
        public async Task<IActionResult> DeleteRequest(int requestId)

        {

            var request = await _context.BookRequests.FindAsync(requestId);

            if (request == null)

                return NotFound();

            if(request.Status == "Accepted")
            {
                return BadRequest("Cannot delete a book request after it has been accepted");
            }

            _context.BookRequests.Remove(request);

            await _context.SaveChangesAsync();



            return NoContent();

        }

    }
}
