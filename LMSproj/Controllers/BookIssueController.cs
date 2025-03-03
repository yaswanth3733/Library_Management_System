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
    public class BookIssueController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BookIssueController(LibraryContext context)
        {
            _context = context;
        }




        // GET: api/BookIssue/all

        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<BookIssueDto>>> GetAllBookIssues()

        {

            var issues = await _context.BookIssues.ToListAsync();

            var issueDtos = issues.Select(issue => new BookIssueDto

            {

                IssueId = issue.IssueId,

                UserId = issue.UserId,

                BookId = issue.BookId,

                IssueDate = issue.IssueDate,

                DueDate = issue.DueDate,

                ReturnDate = issue.ReturnDate,

                FineAmount = issue.FineAmount

            }).ToList();



            return Ok(issueDtos);

        }

        // PUT: api/BookIssue/return

        [HttpPut("return")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ReturnBook([FromBody] ReturnBookDto returnDto)
        {
            var bookIssue = await _context.BookIssues.FindAsync(returnDto.IssueId);
            if (bookIssue == null)
            {
                return NotFound();
            }

            if (bookIssue.ReturnDate != null)
            {
                return BadRequest("Book has already been returned.");
            }

            // Calculate fine if returned late
            bookIssue.ReturnDate = returnDto.ReturnDate;
            if (bookIssue.ReturnDate > bookIssue.DueDate)
            {
                int daysLate = (bookIssue.ReturnDate.Value - bookIssue.DueDate).Days;
                bookIssue.FineAmount = daysLate * 5;  // Fine = ₹5 per late day
            }
            else
            {
                bookIssue.FineAmount = 0m;
            }

            // Increase available book copies
            var book = await _context.Books.FindAsync(bookIssue.BookId);
            if (book != null)
            {
                book.AvailableCopies++;
            }
            var user = await _context.Users.FindAsync(bookIssue.UserId);
            _context.UserActivityLogs.Add(new UserActivityLog
            {
                Username = user.FullName,
                BookName = book.Title,
                Action = "Returned",
                Description = $"Book '{book.Title}' was returned by '{user.FullName}'. Fine paid: {bookIssue.FineAmount}.",
                Timestamp = DateTime.Now,
                FineAmount = bookIssue.FineAmount
            });

            await _context.SaveChangesAsync();

            // Add Fine if applicable
            if (bookIssue.FineAmount > 0)
            {
                _context.Fines.Add(new Fine
                {
                    IssueId = bookIssue.IssueId,
                    FineAmount = bookIssue.FineAmount,
                    IsPaid = false
                });
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
