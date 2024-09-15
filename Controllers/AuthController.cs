using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using BookStore.Models;

namespace BookStore.Controllers
{
	
	[Route("api/[Controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly UserManager<User> _usermanager;
		private readonly RoleManager<Role> _rolemanager;
		
		public AuthController(UserManager<User> usermanager , RoleManager<Role> rolemanager)
		{
			_usermanager = usermanager;
			_rolemanager = rolemanager;
		}
		
		[HttpPost("register")]
		public async Task<IActionResult> Register ([FromBody] RegisterModel model)
		{
			var user = new User
			{
				UserName = model.UserName,
				Email = model.Email,
				FirstName = model.FirstName,
				LastName = model.LastName,
				Role = "User"
			};
			
			var result = await _usermanager.CreateAsync(user , model.Password);
			if (result.Succeeded)
			{
				// the user model is added to the User Table , just an assumption tho
				await _usermanager.AddToRoleAsync(user, "User");
				return Ok("Registered!");
			}
			
			return BadRequest(result.Errors);
			
		}
	
	}
	
	public class RegisterModel 
	{
		public string Email { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		
		// registermodel having a role is goofy , need to dive deeper tho
		//public string Role { get; set; }
	}
}