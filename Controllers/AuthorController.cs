using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
namespace BookStore.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthorsController : ControllerBase
{
	private readonly BookStoreDbContext _context;
	
	public AuthorsController(BookStoreDbContext context)
	{
		_context = context;
	}
	

}