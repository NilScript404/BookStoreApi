using Microsoft.AspNetCore.Mvc;
using BookStore.Models;
using BookStore.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.BookController
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
		public async Task<IEnumerable<Book>> GetAllBooks()
		{
			return await _context.Books.ToListAsync();
		}
		
		[HttpGet("{id}")]
		public async Task<ActionResult<Book>> GetBook([FromRoute] int id)
		{
			var book = await _context.Books.FindAsync(id);
			if (book == null)
			{
				return NotFound();
			}
			return book;
		}
		
		[HttpPost()]
		public async Task<ActionResult<Book>> PostBook( Book book)
		{
				
			_context.Books.Add(book);
			await _context.SaveChangesAsync();
			return Ok();
		}
		
		[HttpDelete("id")]
		public async Task<ActionResult> DeleteBook([FromRoute] int id)
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
		[HttpPut("{id}")]
		public async Task<ActionResult> UpdateBook([FromRoute] int id, [FromBody] Book book)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var NewBook = await _context.Books.FindAsync(id);
			if (NewBook == null)
			{
				return NotFound();
			}
			NewBook.Author = book.Author;
			NewBook.AuthorId = book.AuthorId;
			NewBook.Price = book.Price;
			NewBook.Rating = book.Rating;
			NewBook.PublicationDate = book.PublicationDate;
			
			await _context.SaveChangesAsync();
			return NoContent();
		}
	
	
	
	
	
	
	
	
	
	
	
	





	}
}