using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace JsonCrud.Person;

public static class PersonEndpoints
{
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = true
    };

    public static void MapPersonEndpoints(this WebApplication app)
    {
        app.MapGet("/api/people", async () =>
        {
            try
            {
                var filePath = GetPath();
                List<Person> people = [.. await GetPeople(filePath)];
                return Results.Ok(people);
            }
            catch (Exception ex)
            {
                Console.WriteLine("====>" + ex.Message);
                return Results.StatusCode(500);
            }
        });

        app.MapPost("/api/people", async (CreatePersonDto person) =>
        {
            try
            {
                var filePath = GetPath();
                Person personToCreate = Person.Create(person.FirstName, person.LastName);
                List<Person> updatedPeople = [.. await GetPeople(filePath), personToCreate];
                await UpdateJsonFile(updatedPeople);
                return Results.CreatedAtRoute("GetPersonById", new { id = personToCreate.Id }, personToCreate);
            }
            catch (Exception ex)
            {
                return Results.InternalServerError(ex.Message);
            }
        });

        app.MapGet("/api/people/{id:guid}", async (Guid id) =>
        {
            try
            {
                List<Person> people = [.. await GetPeople(GetPath())];
                var person = people.FirstOrDefault(p => p.Id == id);
                if (person == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(person);
            }
            catch (Exception ex)
            {
                return Results.InternalServerError(ex.Message);
            }
        }).WithName("GetPersonById");

        app.MapPut("/api/people/{id:guid}", async (Guid id, [FromBody] UpdatePersonDto personToUpdate) =>
        {
            if (id != personToUpdate.Id)
            {
                return Results.BadRequest("Ids mismatch");
            }
            try
            {
                List<Person> people = [.. await GetPeople(GetPath())];
                var person = people.SingleOrDefault(a => a.Id == id);
                if (person == null)
                {
                    return Results.NotFound();
                }
                person.Update(personToUpdate.FirstName, personToUpdate.LastName);

                var updatedPeople = people.Select(p => p.Id == person.Id ? person : p);

                await UpdateJsonFile(updatedPeople);

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.InternalServerError(ex.Message);
            }
        });

        app.MapDelete("/api/people/{id:guid}", async (Guid id) =>
        {

            try
            {
                List<Person> people = [.. await GetPeople(GetPath())];
                var person = people.SingleOrDefault(a => a.Id == id);
                if (person == null)
                {
                    return Results.NotFound();
                }

                var updatedPeople = people.Where(p => p.Id != id);

                await UpdateJsonFile(updatedPeople);

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.InternalServerError(ex.Message);
            }
        });

    }


    private static async Task UpdateJsonFile(IEnumerable<Person> updatedPeople)
    {
        var updatedJson = JsonSerializer.Serialize(updatedPeople, _jsonOptions);
        await File.WriteAllTextAsync(GetPath(), updatedJson);
    }

    private static async Task<IEnumerable<Person>> GetPeople(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new InvalidOperationException("FilePath does not found");
        }
        string jsonContent = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<List<Person>>(jsonContent, _jsonOptions) ?? [];
    }

    private static string GetPath()
    {
        return Path.Join("Db", "People.json");
    }
}
