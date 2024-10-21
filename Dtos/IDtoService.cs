using BookStore.Models;
using BookStore.Dto;

namespace BookStore.DtoService {
	public interface IDtoService {
		BookDto MapBookToBookDto(Book book);
		Book MapBookDtoToBook(BookDto bookDto);
		Book MapBookDtoToBookUpdate(BookDto bookDto, Book existingBook);
		
	}
}