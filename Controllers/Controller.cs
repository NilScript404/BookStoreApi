
using Microsoft.AspNetCore.Mvc;
using BookStore.Models;
using BookStore.Data;
using Microsoft.EntityFrameworkCore;
namespace BookStore.AuthorController
{
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
}