using System.Data;
using Dapper;
using DapperMysql.Api.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace DapperMysql.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PeopleController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly string _connectionString;

    public PeopleController(IConfiguration config)
    {
        _config = config;
        _connectionString = _config.GetConnectionString("default") ?? throw new InvalidOperationException("Connection string does not found");
    }

    [HttpGet()]
    public async Task<IActionResult> GetPeople()
    {
        try
        {
            using IDbConnection connection = new MySqlConnection(_connectionString);
            // Note, using statement take care of disposing the connection
            string query = "select Id, FirstName, LastName from People;";
            var people = await connection.QueryAsync<PersonRead>(query);
            return Ok(people);
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
            using IDbConnection connection = new MySqlConnection(_connectionString);
            string query = "select Id, FirstName, LastName from People where Id=@id";
            PersonRead? person = await connection.QuerySingleOrDefaultAsync<PersonRead>(query, new { id });

            // check for not found and return status code accordingly
            if (person is null)
            {
                return NotFound($"Person with id: {id} does not found.");
            }
            return Ok(person);
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
            using IDbConnection connection = new MySqlConnection(_connectionString);
            string query = @"insert into People(FirstName,LastName)
             values(@FirstName,@LastName); select last_insert_id();";
            int createdId = connection.ExecuteScalar<int>(query, personCreate);
            PersonRead createdPerson = new()
            {
                Id = createdId,
                FirstName = personCreate.FirstName,
                LastName = personCreate.LastName
            };
            return CreatedAtRoute("GetPerson", new { id = createdId }, createdPerson); // GetPerson is a Name property defined in GetPerson() method's HttpGet() Attribute
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
            using IDbConnection connection = new MySqlConnection(_connectionString);

            // confirm person with this exists
            int personCount = await connection.ExecuteScalarAsync<int>("select count(1) from People where Id=@id", new { id });

            // check for not found and return status code accordingly
            if (personCount == 0)
            {
                return NotFound($"Person with id: {id} does not found.");
            }

            string updateQuery = @"
            update People 
            set FirstName=@FirstName, 
            LastName=@LastName 
            where Id=@Id; 
            ";
            await connection.ExecuteAsync(updateQuery, personUpdate);
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
            using IDbConnection connection = new MySqlConnection(_connectionString);

            // confirm person with this exists
            int personCount = await connection.ExecuteScalarAsync<int>("select count(1) from People where Id=@id", new { id });

            // check for not found and return status code accordingly
            if (personCount == 0)
            {
                return NotFound($"Person with id: {id} does not found.");
            }

            string deleteQuery = "delete from People where Id=@Id;";
            await connection.ExecuteAsync(deleteQuery, new { id });
            return NoContent(); // returns 204 NoContent status code
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
