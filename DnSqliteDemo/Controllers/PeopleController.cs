using DnSqliteDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace DnSqliteDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PeopleController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetPeople()
        {
            IQueryable<Person> people = _context.People.Take(2);
            return Ok(people);
        }
    }
}
