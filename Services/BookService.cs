using BookStore.Models;
using BookStore.Dto;
using BookStore.BookRepositoryService;
using BookStore.DtoService;

namespace BookStore.BookService
{
	public class BookService : IBookService
	{
		private readonly IBookRepository _repository;
		private readonly IDtoService _dtoService;
		public BookService(IBookRepository repository , IDtoService dtoService)
		{
			_repository = repository;
			_dtoService = dtoService;
		}
		
		public async Task<IEnumerable<BookDto>> GetBooksAsync()
		{
			var books = await _repository.GetBooksAsync();
			return books.Select(b => _dtoService.MapBookToBookDto(b));
		}
		
		public async Task<IEnumerable<BookDto>> GetUserBooksAsync(string userId)
		{
			var books = await _repository.GetBookByUserId(userId);
			return books == null ? null : books.Select(b => _dtoService.MapBookToBookDto(b));
		}
		
		public async Task<BookDto> GetBookAsync(string title, decimal version)
		{
			var book = await _repository.GetBookAsync(title, version);
			return book == null ? null : _dtoService.MapBookToBookDto(book);
		}
		
		public async Task<BookDto> CreateBookAsync(BookDto bookDto, string userId)
		{
			var book = _dtoService.MapBookDtoToBook(bookDto);
			// hardcoded , but who cares its a single line of code
			book.UserId = userId;
			
			await _repository.AddBookAsync(book);
			return _dtoService.MapBookToBookDto(book);
		}
		
		public async Task<Book> UserGetBook(string title, decimal version)
		{
			return await _repository.GetBookAsync(title, version);
		}
		
		public async Task UpdateBookAsync(string title, decimal version, BookDto bookDto)
		{
			var existingBook = await _repository.GetBookAsync(title, version);
			if (existingBook == null) return;
			
			var updatedBook = _dtoService.MapBookDtoToBookUpdate(bookDto, existingBook);
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
			return books == null ? null : books.Select(b => _dtoService.MapBookToBookDto(b));
		}
	}
}
