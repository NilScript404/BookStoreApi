using BookStore.Models;
using BookStore.Dto;
using BookStore.BookRepositoryService;
using BookStore.DtoService;

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
			return MapBooksToDto(books);
		}
		
		public async Task<IEnumerable<BookDto>> GetUserBooksAsync(string userId)
		{
			var books = await _repository.GetBookByUserId(userId);
			return books == null ? null : MapBooksToDto(books);
		}
		
		public async Task<BookDto> GetBookAsync(string title, decimal version)
		{
			var book = await _repository.GetBookAsync(title, version);
			return book == null ? null : MapSingleBookToDto(book);
		}
		
		public async Task<BookDto> CreateBookAsync(BookDto bookDto, string userId)
		{
			var book = MapBookDtoToBook(bookDto);
			// hardcoded , but who cares its a single line of code
			book.UserId = userId;
			
			await _repository.AddBookAsync(book);
			return MapSingleBookToDto(book);
		}
		
		public async Task<Book> UserGetBook(string title, decimal version)
		{
			return await _repository.GetBookAsync(title, version);
		}
		
		public async Task UpdateBookAsync(string title, decimal version, BookDto bookDto)
		{
			var existingBook = await _repository.GetBookAsync(title, version);
			if (existingBook == null) return;
			
			var updatedBook = MapBookDtoToBook(bookDto, existingBook);
			await _repository.UpdateBookAsync(updatedBook);
		}
		
		public async Task DeleteBookAsync(string title, decimal version)
		{
			var book = await _repository.GetBookAsync(title, version);
			if (book != null)
			{
				await _repository.DeleteBookAsync(book);
			}
		}
		
		public async Task<IEnumerable<BookDto>> GetSearchedBooks(string genre)
		{
			var books = await _repository.SearchByGenre(genre);
			return books == null ? null : MapBooksToDto(books);
		}
		
		private IEnumerable<BookDto> MapBooksToDto(IEnumerable<Book> books)
		{
			return books.Select(MapSingleBookToDto);
		}
		
		// Helper function for converting a single Book , to a single BookDto
		// could or maybe should write them in a seperate file
		private BookDto MapSingleBookToDto(Book book)
		{
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
		// Helper function for converting a single BookDto to a single book
		private Book MapBookDtoToBook(BookDto bookDto)
		{
			return new Book
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
		}
		
		// Helper functions for updating the book		
		private Book MapBookDtoToBook(BookDto bookDto, Book existingBook)
		{
			existingBook.Title = bookDto.Title;
			existingBook.Description = bookDto.Description;
			existingBook.PublicationDate = bookDto.PublicationDate;
			existingBook.Price = bookDto.Price;
			existingBook.Rating = bookDto.Rating;
			existingBook.Version = bookDto.Version;
			
			foreach (var genreDto in bookDto.GenreDtos)
			{
				if (!existingBook.Genres.Any(g => g.Name == genreDto.Name))
				{
					existingBook.Genres.Add(new Genre { Name = genreDto.Name });
				}
			}
			
			foreach (var authorDto in bookDto.AuthorDtos)
			{
				var existingAuthor = existingBook.Authors.FirstOrDefault(a =>
					a.FirstName == authorDto.FirstName && a.LastName == authorDto.LastName);
				
				if (existingAuthor != null)
				{
					existingAuthor.FirstName = authorDto.FirstName;
					existingAuthor.LastName = authorDto.LastName;
					existingAuthor.DateOfBirth = authorDto.DateOfBirth;
					existingAuthor.Bio = authorDto.Bio;
				}
				else
				{
					existingBook.Authors.Add(new Author
					{
						FirstName = authorDto.FirstName,
						LastName = authorDto.LastName,
						DateOfBirth = authorDto.DateOfBirth,
						Bio = authorDto.Bio
					});
				}
			}
			
			return existingBook;
		}
	}
}
