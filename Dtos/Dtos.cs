using System.ComponentModel.DataAnnotations;

namespace BookStore.Dto
{
    public class BookDto
    {
        [Required(ErrorMessage = "The Book Needs a Title")]
        [StringLength(50)]
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        [Range(1000, 2024, ErrorMessage = "The publication date must be between 1000 and 2024")]
        public int PublicationDate { get; set; }
        
        [Range(1, 100, ErrorMessage = "The Price should at least be 1$ and 100$ at max")]
        public decimal Price { get; set; }
        
        [Range(1, 10, ErrorMessage = "The rating must be from 1 to 10")]
        public int Rating { get; set; }
        
        public int Version { get; set; }
        
        public List<BookAuthorDto> AuthorDtos { get; set; }
        public List<BookGenreDto> GenreDtos { get; set; }
    }
    
    public class BookAuthorDto
    {
        [Required(ErrorMessage = "Author needs a firstname")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Author needs a lastname")]
        public string LastName { get; set; }
        
        public string Bio { get; set; }
        public int DateOfBirth { get; set; }
    }
    
    public class BookGenreDto
    {
        [Required(ErrorMessage = "The Genre needs a name")]
        public string Name { get; set; }
    }
    // overrides the default scheme , can be further modified
    public class RegisterModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}