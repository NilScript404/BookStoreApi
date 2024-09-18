using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;
using BookStore.BookService;
using BookStore.Dto;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        
        public BooksController(IBookService BookService)
        {
            _bookService = BookService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
        {
            var books = await _bookService.GetBooksAsync();
            return books.Any() ? Ok(books) : NotFound();
        }
        
        [HttpGet("UserBook")]
        [Authorize(Roles = "User")]
        public async  Task<ActionResult<IEnumerable<BookDto>>> GetUserBooks()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            var books = await _bookService.GetUserBooksAsync(userId);
            return books == null ? NotFound("No Books") : Ok(books);
        }
        
        [HttpGet("{Title}/{Version:decimal}")]
        public async Task<ActionResult<BookDto>> GetBook(string Title, decimal Version)
        {
            var book = await _bookService.GetBookAsync(Title, Version);
            return book == null ? NotFound() : Ok(book);
        }
        
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<BookDto>> PostBook(BookDto book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            await _bookService.CreateBookAsync(book, userId);
            return Ok(book);
        }
        
        
        [HttpPut("{Title}/{Version}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> PutBook(string Title, decimal Version, BookDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            var book = await _bookService.UserGetBook(Title, Version);
            
            if (book == null)
            {
                return NotFound("the book was not found");
            }
            if (book.UserId != userId)
            {
                return Unauthorized("You are not allowed to update this book");
            }
            
            await _bookService.UpdateBookAsync(Title, Version, bookDto);
            return NoContent();
        }
        

        // todo
        // User should only be capable of deleting his own books
        [HttpDelete("{Title}/{Version:decimal}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBook(string Title, decimal Version)
        {
            await _bookService.DeleteBookAsync(Title, Version);
            return NoContent();
        }
    
    }
}