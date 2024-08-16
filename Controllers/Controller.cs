using Microsoft.AspNetCore.Mvc;
using BookStore.Models;
using BookStore.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BookStoreDbContext _context;

        public BooksController(BookStoreDbContext context)
        {
            _context = context;
        }

        // GET api/books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            try
            {
                var books = await _context.Books
                    .Include(b => b.Author)
                    .Include(b => b.Genres)
                    .ToListAsync();

                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving books: " + ex.Message);
            }
        }

        // GET api/books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            try
            {
                var book = await _context.Books
                    .Include(b => b.Author)
                    .Include(b => b.Genres)
                    .FirstOrDefaultAsync(b => b.Id == id);

                if (book == null)
                {
                    return NotFound();
                }

                return Ok(book);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving book: " + ex.Message);
            }
        }

        // POST api/books
        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(Book book)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _context.Books.Add(book);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(400, "Error creating book: " + ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error creating book: " + ex.Message);
            }
        }

        // PUT api/books/id
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBook(int id, Book book)
        {
            try
            {
                if (id != book.Id)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _context.Entry(book).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(400, "Error updating book: " + ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error updating book: " + ex.Message);
            }
        }

        // DELETE api/books/id
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            try
            {
                var book = await _context.Books
                    .Include(b => b.Author)
                    .Include(b => b.Genres)
                    .FirstOrDefaultAsync(b => b.Id == id);

                if (book == null)
                {
                    return NotFound();
                }

                _context.Books.Remove(book);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error deleting book: " + ex.Message);
            }
        }
    }
}