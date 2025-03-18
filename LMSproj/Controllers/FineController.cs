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
    public class FineController : ControllerBase
    {
      

            private readonly LibraryContext _context;



            public FineController(LibraryContext context)
        {
            _context = context;
        }


        [HttpGet("all")]

        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<IEnumerable<FineDto>>> GetAllFines()

        {

            var fines = await _context.Fines

                .Include(f => f.BookIssue)

                .ThenInclude(bi => bi.User)

                .Include(bi => bi.BookIssue.Book) // Ensure Book Title is included

                .ToListAsync();

            var fineDtos = fines.Select(f => new FineDto

            {

                FineId = f.FineId,

                IssueId = f.IssueId,

                UserId = f.BookIssue.UserId,

                UserName = f.BookIssue.User.FullName,

                BookId = f.BookIssue.BookId,

                Title = f.BookIssue.Book.Title,

                FineAmount = f.FineAmount,

                IsPaid = f.IsPaid

            }).ToList();

            return Ok(fineDtos);

        }
        // ✅ 1. View Unpaid Fines for a User

        [HttpGet("user/{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<FineDto>>> GetUserFines(int userId)

            {

                     var fines = await _context.BookIssues

                    .Where(bi => bi.UserId == userId && bi.FineAmount > 0) 

                    .Select(bi => new FineDto

                    {

                        IssueId = bi.IssueId,

                        UserId = bi.UserId,

                        BookId = bi.BookId,

                        FineAmount = bi.FineAmount,

                        IsPaid = bi.IsFinePaid

                    })

                    .ToListAsync();



                if (!fines.Any())

                    return NotFound("No pending fines for this user.");



                return Ok(fines);

            }

        //[HttpGet("unpaid-fines")]
        //[Authorize(Roles ="Admin")]
        //public async Task<ActionResult<List<UserUnpaidFineDto>>> GetUsersWithUnpaidFines()

        //{

        //    var unpaidFines = await _context.BookIssues

        //        .Where(b => b.FineAmount > 0 && !b.IsFinePaid)

        //        .Select(b => new

        //        {

        //            b.UserId,

        //            b.User.FullName,

        //            b.User.Email,

        //            b.FineAmount

        //        })

        //        .ToListAsync(); // ✅ Fetch first, then process



        //    var groupedFines = unpaidFines

        //        .GroupBy(b => b.UserId)

        //        .Select(g => new UserUnpaidFineDto

        //        {

        //            UserId = g.Key,

        //            FullName = g.First().FullName,

        //            Email = g.First().Email,

        //            TotalUnpaidFine = g.Sum(b => b.FineAmount)

        //        })

        //        .ToList(); // ✅ Process in-memory



        //    return Ok(groupedFines);
        //}

            // ✅ 2. Pay Fine for a Specific Issue

        [HttpPost("pay/{issueId}")]
        [Authorize (Roles ="Admin")]
        public async Task<IActionResult> PayFine(int issueId)

            {
            var fineobj = await _context.Fines.FirstOrDefaultAsync(f => f.IssueId == issueId);
                if (fineobj == null)
                {
                    return NotFound("Fine record not found.");
                }
                if (fineobj.IsPaid)
                {
                    return BadRequest("Fine has already been paid.");
                }
            var bookIssue = await _context.BookIssues.FindAsync(issueId);

                if (bookIssue == null)

                    return NotFound("Book issue record not found.");



                if (bookIssue.FineAmount == 0)

                    return BadRequest("No fine is due for this book issue.");


                    decimal paidFine = bookIssue.FineAmount;
                    bookIssue.FineAmount = 0;
                    bookIssue.IsFinePaid = true;

                    fineobj.IsPaid = true;
                    fineobj.PaymentDate = DateTime.Now;
                    await _context.SaveChangesAsync();

                return Ok(new { message = "Fine paid successfully." });

            }
        }
}
