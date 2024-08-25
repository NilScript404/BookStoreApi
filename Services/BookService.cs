using BookStore.Data;
using BookStore.Models;
using BookStore.Dto;
using Microsoft.EntityFrameworkCore;

namespace BookStore.BookService
{
	public class BookService : IBookService
	{
		private readonly BookStoreDbContext _context;
		
		public BookService(BookStoreDbContext context)
		{
			_context = context;
		}
		
		public async Task<IEnumerable<BookDto>> GetBooksAsync()
		{
			var books = await _context.Books
				.Include(b => b.Authors)
				.Include(g => g.Genres)
				.ToListAsync();
			
			if (books == null)
			{
				return null;
			}
			
			return books.Select(b => new BookDto
			{
				Title = b.Title,
				Description = b.Description,
				PublicationDate = b.PublicationDate,
				Price = b.Price,
				Rating = b.Rating,
				
				AuthorDtos = b.Authors.Select(a => new BookAuthorDto
				{
					FirstName = a.FirstName,
					LastName = a.LastName,
					DateOfBirth = a.DateOfBirth,
					Bio = a.Bio
				}).ToList(),
				
				GenreDtos = b.Genres.Select(g => new BookGenreDto
				{
					Name = g.Name
				}).ToList()
			});
		}
		
		public async Task<BookDto> GetBookAsync(string Title)
		{
			var book = await _context.Books
				.Include(b => b.Authors)
				.Include(g => g.Genres)
				.FirstOrDefaultAsync(t => t.Title == Title);
			
			if (book == null)
			{
				return null;
			}
			
			return new BookDto
			{
				Title = book.Title,
				Description = book.Description,
				PublicationDate = book.PublicationDate,
				Price = book.Price,
				Rating = book.Rating,
				AuthorDtos = book.Authors.Select(a => new BookAuthorDto
				{
					FirstName = a.FirstName,
					LastName = a.LastName,
					DateOfBirth = a.DateOfBirth,
					Bio = a.Bio
				}).ToList(),
				
				GenreDtos = book.Genres.Select(g => new BookGenreDto
				{
					Name = g.Name
				}).ToList()
			};
		}
		
		public async Task<BookDto> CreateBookAsync(BookDto bookDto)
		{
			
			var book = new Book
			{
				Title = bookDto.Title,
				Description = bookDto.Description,
				PublicationDate = bookDto.PublicationDate,
				Price = bookDto.Price,
				Rating = bookDto.Rating,
				
				Authors = bookDto.AuthorDtos.Select(a => new Author
				{
					FirstName = a.FirstName,
					LastName = a.LastName,
					DateOfBirth = a.DateOfBirth,
					Bio = a.Bio
				}).ToList(),
				
				Genres = bookDto.GenreDtos.Select(g => new Genre
				{
					Name = g.Name
				}).ToList()
			};
			
			_context.Books.Add(book);
			await _context.SaveChangesAsync();
			
			return new BookDto
			{
				Title = book.Title,
				Description = book.Description,
				PublicationDate = book.PublicationDate,
				Price = book.Price,
				Rating = book.Rating,
				AuthorDtos = book.Authors.Select(a => new BookAuthorDto
				{
					FirstName = a.FirstName,
					LastName = a.LastName,
					DateOfBirth = a.DateOfBirth,
					Bio = a.Bio
				}).ToList(),
				GenreDtos = book.Genres.Select(g => new BookGenreDto
				{
					Name = g.Name
				}).ToList()
			};
		}
		
		public async Task UpdateBookAsync(string Title, BookDto bookDto)
		{
			
			var existingbook = await _context.Books
			.Include(a => a.Authors)
			.Include(g => g.Genres)
			.FirstOrDefaultAsync(t => t.Title == Title);
			
			if (existingbook == null)
			{
				return;
			}
			existingbook.Title = bookDto.Title;
			existingbook.Description = bookDto.Description;
			existingbook.PublicationDate = bookDto.PublicationDate;
			existingbook.Price = bookDto.Price;
			existingbook.Rating = bookDto.Rating;
			
			foreach (var genre in bookDto.GenreDtos)
			{
				var existinggenre = existingbook.Genres.FirstOrDefault(g => g.Name == genre.Name);
				
				// kidna useless ?
				if (existinggenre != null)
				{
					existinggenre.Name = genre.Name;
				}
				else
				{
					existingbook.Genres.Add(new Genre { Name = genre.Name });
				}
			}
			
			foreach (var author in bookDto.AuthorDtos)
			{
				
				var existingauthor = existingbook.Authors.FirstOrDefault
					(a => a.FirstName + " " + a.LastName == author.FirstName + " " + author.LastName);
				
				if (existingauthor != null)
				{
					existingauthor.FirstName = author.FirstName;
					existingauthor.LastName = author.LastName;
					existingauthor.DateOfBirth = author.DateOfBirth;
					existingauthor.Bio = author.Bio;
				}
				else
				{
					existingbook.Authors.Add(new Author
					{
						FirstName = author.FirstName,
						LastName = author.LastName,
						DateOfBirth = author.DateOfBirth,
						Bio = author.Bio
					});
				}
			
			}
			
			await _context.SaveChangesAsync();
		}
		
		public async Task DeleteBookAsync(string Title )
		{
			var book = await _context.Books.FirstOrDefaultAsync(b => b.Title == Title);
			if (book == null)
			{
				return;
			}
			
			_context.Books.Remove(book);
			await _context.SaveChangesAsync();
		}
	}
}