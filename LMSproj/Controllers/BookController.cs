using LMSproj.DTO;
using LMSproj.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace LMSproj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BookController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/Book/all
        [HttpGet("all")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAllBooks()
        {
            var books = await _context.Books.ToListAsync();

            var bookDtos = books.Select(b => new BookDto
            {
                BookId = b.BookId,
                Title = b.Title,
                Author = b.Author,
                ISBN = b.ISBN,
                TotalCopies = b.TotalCopies,
                AvailableCopies = b.AvailableCopies,
                Category = b.Category
            }).ToList();

            return Ok(bookDtos);
        }

         // ✅ 3. Get Unique Book Categories (For Dropdown)

        [HttpGet("categories")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<string>>> GetCategories()

        {

            var categories = await _context.Books

                .Select(b => b.Category)

                .Distinct()

                .ToListAsync();



            return Ok(categories);

        }



        // ✅ 4. Search Books by Title or Category

        [HttpGet("search")]

        public async Task<ActionResult<IEnumerable<BookDto>>> SearchBooks([FromQuery] string? title, [FromQuery] string? category)

        {

            var query = _context.Books.AsQueryable();



            if (!string.IsNullOrEmpty(title))

                query = query.Where(b => b.Title.Contains(title));



            if (!string.IsNullOrEmpty(category))

                query = query.Where(b => b.Category == category);



            var books = await query.Select(b => new BookDto

            {

                BookId = b.BookId,

                Title = b.Title,

                Author = b.Author,
                
                ISBN = b.ISBN,

                TotalCopies = b.TotalCopies,

                AvailableCopies = b.AvailableCopies,

                Category = b.Category

            }).ToListAsync();



            return books.Any() ?Ok(books):NotFound("No books found.");

        }





        // GET: api/Book/{id}
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<BookDto>> GetBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            var bookDto = new BookDto
            {
                BookId = book.BookId,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                TotalCopies = book.TotalCopies,
                AvailableCopies = book.AvailableCopies,
                Category = book.Category
            };

            return Ok(bookDto);
        }

        // POST: api/Book/add (Admin Only)
        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BookDto>> AddBook([FromBody] AddBookDto addBookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var book = new Book
            {
                Title = addBookDto.Title,
                Author = addBookDto.Author,
                ISBN = addBookDto.ISBN,
                TotalCopies = addBookDto.TotalCopies,
                AvailableCopies = addBookDto.TotalCopies, // Initially all copies are available
                Category = addBookDto.Category
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookById), new { id = book.BookId }, book);
        }

        // PUT: api/Book/update/{id} (Admin Only)
        [HttpPut("update/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookDto updateBookDto)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            book.Title = updateBookDto.Title;
            book.Author = updateBookDto.Author;
            book.ISBN = updateBookDto.ISBN;
            book.TotalCopies = updateBookDto.TotalCopies;
            book.AvailableCopies = updateBookDto.AvailableCopies;
            book.Category = updateBookDto.Category;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Book/delete/{id} (Admin Only)
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            bool isIssued = await _context.BookIssues.AnyAsync(bi => bi.BookId == id && bi.ReturnDate == null);
            if (isIssued)
            {
                return BadRequest("Cannot delete this book. It has been issued and not yet returned.");
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        
    }
    }
