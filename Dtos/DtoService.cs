using BookStore.Models;
using BookStore.Dto;
using BookStore.BookRepositoryService;

namespace BookStore.DtoService
{
	class DtoService
	{
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