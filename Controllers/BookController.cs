using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Data;
using BookStore.Models;

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
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            var books = await _context.Books
                .Include(b => b.Authors)
                .Include(g => g.Genres)
                .ToListAsync();
            
            return Ok(books);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books
                .Include(b => b.Authors)
                .Include(bg => bg.Genres)
                .FirstOrDefaultAsync(b => b.Id == id);
            
            if (book == null)
            {
                return NotFound();
            }
            
            return Ok(book);
        }
        
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }
            
            var existingbook = await _context.Books
                .Include(a => a.Authors)
                .Include(bg => bg.Genres)
                .FirstOrDefaultAsync(b => b.Id == id);
                
            if (existingbook == null)
            {
                return NotFound();
            }
            
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            
            existingbook.Title = book.Title;
            existingbook.Description = book.Description;
            existingbook.PublicationDate = book.PublicationDate;
            existingbook.Price = book.Price;
            existingbook.Rating = book.Rating;
            
            
            foreach (var genre in book.Genres)
            {
                var existinggenre = existingbook.Genres.FirstOrDefault(g => g.Name == genre.Name);
                
                if (existinggenre != null)
                {
                    existinggenre.Name = genre.Name;
                }
                else {
                    existingbook.Genres.Add(genre);
                }
                
            }
            
            foreach(var author in book.Authors)
            {
                var existingauthor = existingbook.Authors.FirstOrDefault(a => a.Id == author.Id);
                
                if (existingauthor != null)
                {
                    existingauthor.FirstName = author.FirstName;
                    existingauthor.LastName = author.LastName;
                    existingauthor.DateOfBirth = author.DateOfBirth;
                }
                else 
                {
                    existingbook.Authors.Add(author);
                }
            }
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                if (!_context.Books.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }
               
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            
            if (book == null)
            {
                return NotFound();
            }
            
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            
            return NoContent();
        }
        
    }
}
