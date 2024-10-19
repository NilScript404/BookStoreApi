using Microsoft.AspNetCore.Mvc;
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
            return books.Any() ? Ok(books) : Ok("No Book Was Found");
        }
        
        // FromQuery throws badrequest if we dont write a genre => we dont need to check
        // if genre is null or not
        [HttpGet("SearchByGenre")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetSearchedBooks([FromQuery] string genre)
        {
            // works only for a single genre , EX => Action
            // Todo for multiple genres 
            foreach (var genretemp in genres)
            {
                if (genretemp == genre)
                {
                    var books = await _bookService.GetSearchedBooks(genre);
                    return Ok(books.Any() ? books : "No Book Exists with this Genre");
                }
            }
            return BadRequest("This Genre Doesnt Exist");
        }
        
        [HttpGet("UserBook")]
        [Authorize(Roles = "User")]
        public async  Task<ActionResult<IEnumerable<BookDto>>> GetUserBooks()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            var books = await _bookService.GetUserBooksAsync(userId);
            return books == null ? NotFound("The Specified Book Was Not Found") : Ok(books);
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
            foreach(string genre in genres )
            {
                if (book.GenreDtos.Any(x => x.Name != genre)) {
                    return BadRequest(genre + "is not a valid Genre");
                }
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string tempgenre;
            foreach(BookGenreDto temp in book.GenreDtos)
            {
                tempgenre = temp.Name; 
            }
            
            // checking if genre exists in the list of genres
            foreach(var temp in book.GenreDtos)
            {
                bool exist = false;
                foreach(string genre in genres)
                {
                    if (temp.Name == genre)
                    {
                        exist = true;
                        break;
                    }
                }
                if (!exist){
                    return BadRequest($"The Genre {temp} Doesnt Exist");
                }
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
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteBook(string Title, decimal Version)
        {
            await _bookService.DeleteBookAsync(Title, Version);
            return Ok("Book Was Succesfully Deleted");
        }
        
        static List<string> genres = new List<string>
        {
            "Action",
            "Adventure",
            "Animation",
            "Biography",
            "Comedy",
            "Crime",
            "Documentary",
            "Drama",
            "Fantasy",
            "Historical",
            "Horror",
            "Musical",
            "Mystery",
            "Romance",
            "Sci-Fi",
            "Sport",
            "Thriller",
            "War",
            "Western",
            "Family"
        };   
    }
}