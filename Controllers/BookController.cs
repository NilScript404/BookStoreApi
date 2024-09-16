using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;
using BookStore.BookService;
using BookStore.Dto;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Roles = "User")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
        {   
            var books = await _bookService.GetBooksAsync();
            return books.Any() ? Ok(books) : NotFound();
        }
        
        [HttpGet("{Title}/{Version:decimal}")]
        public async Task<ActionResult<BookDto>> GetBook(string Title , decimal Version)
        {
            var book = await _bookService.GetBookAsync(Title , Version);
            return book == null ? NotFound() : Ok(book);
        }
        
        [HttpPost]
        public async Task<ActionResult<BookDto>> PostBook(BookDto book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            await _bookService.CreateBookAsync(book);
            return Ok(book);
        }
        
        [HttpPut("{Title}/{Version}")]
        public async Task<IActionResult> PutBook(string Title, decimal Version , BookDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            await _bookService.UpdateBookAsync(Title , Version , bookDto);
            return NoContent();
        }
           
        [HttpDelete("{Title}/{Version:decimal}")]
        public async Task<IActionResult> DeleteBook(string Title , decimal Version)
        {
            await _bookService.DeleteBookAsync(Title , Version);
            return NoContent();
        }
        
    }
}