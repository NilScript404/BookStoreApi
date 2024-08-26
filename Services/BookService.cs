using BookStore.Data;
using BookStore.Models;
using BookStore.Dto;
using Microsoft.EntityFrameworkCore;
using BookStore.BookRepositoryService;

namespace BookStore.BookService
{
	public class BookService : IBookService
	{
		private readonly IBookRepository _repository;
		
		public BookService(IBookRepository repository)
		{
			_repository = repository;
		}
		
		public async Task<IEnumerable<BookDto>> GetBooksAsync()
		{
			
			var books = await _repository.GetBooksAsync();
			
			return books.Select(b => new BookDto
			{
				Title = b.Title,
				Description = b.Description,
				PublicationDate = b.PublicationDate,
				Price = b.Price,
				Rating = b.Rating,
				Version = b.Version,
				
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
		
		public async Task<BookDto> GetBookAsync(string Title , decimal Version)
		{
			
			var book = await _repository.GetBookAsync(Title, Version);
			
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
				Version = book.Version,
				
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
				Version = bookDto.Version,
				
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
			
			await _repository.AddBookAsync(book);
			
			return new BookDto
			{
				Title = book.Title,
				Description = book.Description,
				PublicationDate = book.PublicationDate,
				Price = book.Price,
				Rating = book.Rating,
				Version = book.Version,
				
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
		
		public async Task UpdateBookAsync(string Title , decimal Version , BookDto bookDto)
		{
			var existingbook = await _repository.GetBookAsync(Title, Version);
			if (existingbook == null)
			{
				return;
			}
			
			existingbook.Title = bookDto.Title;
			existingbook.Description = bookDto.Description;
			existingbook.PublicationDate = bookDto.PublicationDate;
			existingbook.Price = bookDto.Price;
			existingbook.Rating = bookDto.Rating;
			existingbook.Version = bookDto.Version;
			
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
			
			await _repository.UpdateBookAsync(existingbook);
		}
		
		public async Task DeleteBookAsync(string Title , decimal Version )
		{
			var book = await _repository.GetBookAsync(Title, Version);
			if (book == null)
			{
				return;
			}
			
			await _repository.DeleteBookAsync(book);	
		}
	}
}