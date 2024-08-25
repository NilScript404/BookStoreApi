using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;
using BookStore.BookService;
using BookStore.Dto;

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
            try {
            var books = await _bookService.GetBooksAsync();
            return books == null ? NotFound() : Ok(books);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        [HttpGet("{Title}")]
        public async Task<ActionResult<BookDto>> GetBook(string Title)
        {
            var book = await _bookService.GetBookAsync(Title);
            return book == null ? NotFound() : Ok(book);
        }
        
        [HttpPost]
        public async Task<ActionResult<BookDto>> PostBook(BookDto book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try 
            {
            await _bookService.CreateBookAsync(book);
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(book);
        }
        
        [HttpPut("{Title}")]
        public async Task<IActionResult> PutBook(string Title ,  BookDto bookDto)
        {
            
            if (Title != bookDto.Title)
            {
                return BadRequest("The Book Was Not Found");
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try {
            await _bookService.UpdateBookAsync(Title , bookDto);
            return NoContent();
            }
            catch(Exception){
                throw;
            }
        
        }
               
        [HttpDelete("{Title}")]
        public async Task<IActionResult> DeleteBook(string Title)
        {
            try {
            await _bookService.DeleteBookAsync(Title);
            return NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
    }
}
