using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;
using BookStore.Dto;
using BookStore.Data;
using System.Linq;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BookStoreDbContext _context;

        public BooksController(BookStoreDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks()
        {
            var books = await _context.Books
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .ToListAsync();

            var bookDtos = books.Select(b => new BookDTO
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                PublicationDate = b.PublicationDate,
                Price = b.Price,
                Rating = b.Rating,
                Authors = b.BookAuthors.Select(ba => ba.Author).Select(a => new AuthorDTO
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Bio = a.Bio,
                    DateOfBirth = a.DateOfBirth
                }).ToList()
            });

            return Ok(bookDtos);
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetBook(int id)
        {
            var book = await _context.Books
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            var bookDto = new BookDTO
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                PublicationDate = book.PublicationDate,
                Price = book.Price,
                Rating = book.Rating,
                Authors = book.BookAuthors.Select(ba => ba.Author).Select(a => new AuthorDTO
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Bio = a.Bio,
                    DateOfBirth = a.DateOfBirth
                }).ToList()
            };

            return Ok(bookDto);
        }

        // POST: api/Books
        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(CreateBookDTO createBookDto)
        {
            if (createBookDto == null)
            {
                return BadRequest();
            }

            var book = new Book
            {
                Title = createBookDto.Title,
                Description = createBookDto.Description,
                PublicationDate = createBookDto.PublicationDate,
                Price = createBookDto.Price,
                Rating = createBookDto.Rating
            };

            if (createBookDto.Authors != null)
            {
                foreach (var authorDto in createBookDto.Authors)
                {
                    var author = await _context.Authors.FindAsync(authorDto.Id);
                    if (author != null)
                    {
                        book.BookAuthors.Add(new BookAuthor { Author = author });
                    }
                    else
                    {
                        author = new Author
                        {
                            FirstName = authorDto.FirstName,
                            LastName = authorDto.LastName,
                            Bio = authorDto.Bio,
                            DateOfBirth = authorDto.DateOfBirth
                        };
                        _context.Authors.Add(author);
                        book.BookAuthors.Add(new BookAuthor { Author = author });
                    }
                }
            }

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }
        
        // PUT: api/Books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, UpdateBookDTO updateBookDto)
        {
            if (id != updateBookDto.Id)
            {
                return BadRequest();
            }
                
            var book = await _context.Books
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            book.Title = updateBookDto.Title;
            book.Description = updateBookDto.Description;
            book.PublicationDate = updateBookDto.PublicationDate;
            book.Price = updateBookDto.Price;
            book.Rating = updateBookDto.Rating;

            var authorsToRemove = book.BookAuthors.Where(ba => !updateBookDto.AuthorIds.Contains(ba.AuthorId ?? 0)).ToList();
            foreach (var authorToRemove in authorsToRemove)
            {
                book.BookAuthors.Remove(authorToRemove);
            }
            
            var authorsToAdd = updateBookDto.AuthorIds.Where(authorId => !book.BookAuthors.Any(ba => ba.AuthorId == authorId)).ToList();
            foreach (var authorId in authorsToAdd)
            {
                var author = await _context.Authors.FindAsync(authorId);
                if (author != null)
                {
                    book.BookAuthors.Add(new BookAuthor { Author = author });
                }
            }
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
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

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}