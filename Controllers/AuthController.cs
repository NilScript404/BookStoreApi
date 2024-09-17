using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Dto;
namespace BookStore.Controllers
{
	
	[Route("api/[Controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly UserManager<User> _usermanager;
		//	private readonly RoleManager<Role> _rolemanager;
		
		private readonly SignInManager<User> _signinmanager;
		public AuthController(UserManager<User> usermanager , SignInManager<User> signinmanager)
		{
			_usermanager = usermanager;
			//	_rolemanager = rolemanager;
			_signinmanager = signinmanager;
		}
		
		
		// HttpPost
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
	
		[HttpPost("Login")]
		public async Task<IActionResult> Login([FromBody] LoginModel model)
		{
			var user = await _usermanager.FindByNameAsync(model.UserName);
			if (user == null)
			{
				return Unauthorized("User Not Found");
			}
			
			if (await _usermanager.IsLockedOutAsync(user))
			{
				return Forbid("Max Login Attempt reached , try again after 90 minutes");
			}
			
			
			var result = await _signinmanager.PasswordSignInAsync(model.UserName, model.Password, isPersistent: false, lockoutOnFailure: false);
			if (result.Succeeded)
			{
				return Ok("Login Successful");
			}
			if (result.IsLockedOut)
			{
				return Forbid("Account has been Locked");
			}
			
			return Unauthorized("Wrong Password or A Failed Login Attempt");
		}
		
		[HttpPost("Logout")]
		public async Task<IActionResult> Logout(){
			await _signinmanager.SignOutAsync();
			return Ok("Logged out successfully");
		}
	
	}
	
}