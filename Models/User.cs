using Microsoft.AspNetCore.Identity;

namespace BookStore.Models;


// just an example of extending the IdentityUser
public class User : IdentityUser 
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Role { get; set; }
}

// really not needed in our case , just trying to showcase that we can extend the Identitrole
public class Role : IdentityRole
{
	public string? Info { get; set; }
}