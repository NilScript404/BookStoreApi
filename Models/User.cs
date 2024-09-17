using Microsoft.AspNetCore.Identity;
using BookStore.Models;
namespace BookStore.Models;


// just an example of extending the IdentityUser
public class User : IdentityUser 
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public List<Book>? UploadedBooks { get; set; }
	
	public string Role { get; set; }
}

public class Role : IdentityRole
{
}