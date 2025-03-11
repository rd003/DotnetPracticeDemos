using CursorPaginationDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CursorPaginationDemo.Controllers
{
    [ApiController]
    [Route("/api/{controller}")]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("offset")]
        public async Task<IActionResult> GetBooks(int limit=10, int page=1)
        {
            var books = await _context.Books
                .AsNoTracking()
                .OrderBy(a => a.Id)
                .Skip(limit * (page - 1))
                .Take(limit)
                .ToListAsync();
            return Ok(books);
        }

        [HttpGet("keyset")]
        public async Task<IActionResult> GetBooksWithKeyset(int limit = 10, int lastId = 0)
        {
            var books = await _context.Books
                .AsNoTracking()
                .OrderBy(b => b.Id)
                .Where(b=>b.Id>lastId)
                .Take(limit)
                .ToListAsync();
            return Ok(books);
        }


    }
}
