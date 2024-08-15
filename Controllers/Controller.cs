using Microsoft.AspNetCore.Mvc;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using BookStore.Data;

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

		// GET: api/Books
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
		{
			return await _context.Books
				 .Include(b => b.Author)
				 .Include(b => b.Genre)
				 .ToListAsync();
		}

		// GET: api/Books/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Book>> GetBook(int id)
		{
			var book = await _context.Books
				 .Include(b => b.Author)
				 .Include(b => b.Genre)
				 .FirstOrDefaultAsync(b => b.Id == id);

			if (book == null)
			{
				return NotFound();
			}

			return book;
		}

		// POST: api/Books
		[HttpPost]
		public async Task<ActionResult<Book>> CreateBook(Book book)
		{
			_context.Books.Add(book);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
		}

		// PUT: api/Books/5
		[HttpPut("{id}")]
		public async Task<ActionResult> UpdateBook(int id, Book book)
		{
			if (id != book.Id)
			{
				return BadRequest();
			}

			_context.Entry(book).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!await BookExists(id))
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

		// DELETE: api/Books/5
		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteBook(int id)
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

		private async Task<bool> BookExists(int id)
		{
			return await _context.Books.AnyAsync(e => e.Id == id);
		}
	}
}