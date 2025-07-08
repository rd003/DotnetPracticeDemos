
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MinimalApis25.Models;
using MinimalApis25.Validators;

namespace MinimalApis25.Endpoints;

public static class PersonEndpoints
{
    public static void MapPersonEndpoints(this WebApplication app)
    {
        var personApis = app.MapGroup("/api/people");

        personApis.MapGet("/", GetPeople);
        personApis.MapGet("/{id:int}", GetPerson);
        personApis.MapPost("/", CreatePerson);
        personApis.MapPut("/{id:int}", UpdatePerson);
        personApis.MapDelete("/{id:int}", DeletePerson);
    }

    private static async Task<IResult> GetPerson(int id, AppDbContext context)
    {
        try
        {
            var person = await context.People.FindAsync(id);

            if (person is null)
            {
                return TypedResults.NotFound(); // 404 Not found status code
            }

            return TypedResults.Ok(person); // 200 status code + person object
        }
        catch (Exception ex)
        {
            return TypedResults.InternalServerError(ex.Message); // 500 Internal server error + error message in the body
        }
    }

    private static async Task<IResult> CreatePerson(IValidator<Person> validator, Person person, AppDbContext context)
    {
        try
        {
            var validationResult = await validator.ValidateAsync(person);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            context.People.Add(person);
            await context.SaveChangesAsync();
            return TypedResults.Created($"/api/people/{person.Id}", person); // 201 created + location of the resource in header + created person object in the body
        }
        catch (Exception ex)
        {
            return TypedResults.InternalServerError(ex.Message); // 500 Internal server error + error message in the body
        }
    }

    private static async Task<IResult> UpdatePerson(int id, IValidator<Person> validator, Person person, AppDbContext context)
    {
        try
        {
            var validationResult = await validator.ValidateAsync(person);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            if (id != person.Id)
            {
                return TypedResults.BadRequest($"Ids does not match");
            }

            // whether a person exists in the database

            if (!await context.People.AnyAsync(p => p.Id == id))
            {
                return TypedResults.NotFound();
            }

            context.People.Update(person);
            await context.SaveChangesAsync();
            return TypedResults.NoContent(); // 204 No content
        }
        catch (Exception ex)
        {
            return TypedResults.InternalServerError(ex.Message); // 500 Internal server error + error message in the body
        }
    }

    private static async Task<IResult> GetPeople(AppDbContext context)
    {
        try
        {
            var people = await context.People.ToListAsync(); // all the rows and columsn of people table 
            return TypedResults.Ok(people); // 200 status code + array of person
        }
        catch (Exception ex)
        {
            return TypedResults.InternalServerError(ex.Message); // 500 Internal server error + error message in the body
        }
    }

    private static async Task<IResult> DeletePerson(int id, AppDbContext context)
    {
        try
        {
            var person = await context.People.FindAsync(id);

            if (person is null) return TypedResults.NotFound();
            context.People.Remove(person);
            await context.SaveChangesAsync();
            return TypedResults.NoContent(); // 204 No content
        }
        catch (Exception ex)
        {
            return TypedResults.InternalServerError(ex.Message); // 500 Internal server error + error message in the body
        }
    }
}
