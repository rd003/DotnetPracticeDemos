using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlEfCore.Api.DTOs;
using MySqlEfCore.Api.Mappers;
using MySqlEfCore.Api.Models;

namespace MySqlEfCore.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PeopleController : ControllerBase
{
    private readonly AppDbContext _context;
    public PeopleController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet()]
    public async Task<IActionResult> GetPeople()
    {
        try
        {
            var people = await _context.People.AsNoTracking().ToListAsync();
            var peopleRead = people.Select(p => p.ToPersonRead()); // mapped to DTO
            return Ok(peopleRead);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("{id}", Name = "GetPerson")]
    public async Task<IActionResult> GetPerson(int id)
    {
        try
        {
            var person = await _context.People.FindAsync(id);

            // check for not found and return status code accordingly
            if (person is null)
            {
                return NotFound($"Person with id: {id} does not found.");
            }
            return Ok(person.ToPersonRead());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreatePerson(PersonCreate personCreate)
    {
        try
        {
            var person = personCreate.ToPerson();
            _context.People.Add(person);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("GetPerson", new { id = person.Id }, person.ToPersonRead()); // GetPerson is a Name property defined in GetPerson() method's HttpGet() Attribute
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePerson(int id, [FromBody] PersonUpdate personUpdate)
    {
        if (id != personUpdate.Id)
        {
            return BadRequest("Ids mismatch");
        }
        try
        {

            var isPersonExists = _context.People.AsNoTracking().Any(p => p.Id == id);
            // check for not found and return status code accordingly
            if (!isPersonExists)
            {
                return NotFound($"Person with id: {id} does not found.");
            }

            var person = personUpdate.ToPerson();
            _context.People.Update(person);
            await _context.SaveChangesAsync();

            return NoContent(); // returns 204 NoContent status code
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerson(int id)
    {
        try
        {
            var person = await _context.People.FindAsync(id);
            // check for not found and return status code accordingly
            if (person == null)
            {
                return NotFound($"Person with id: {id} does not found.");
            }

            _context.People.Remove(person);
            await _context.SaveChangesAsync();

            return NoContent(); // returns 204 NoContent status code
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

}
