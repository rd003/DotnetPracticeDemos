using MySqlEfCore.Api.DTOs;
using MySqlEfCore.Api.Models;

namespace MySqlEfCore.Api.Mappers;

public static class PersonMapper
{
    public static PersonRead ToPersonRead(this Person person)
    {
        return new PersonRead
        {
            Id = person.Id,
            FirstName = person.FirstName,
            LastName = person.LastName
        };
    }

    public static Person ToPerson(this PersonCreate person)
    {
        return new Person
        {
            FirstName = person.FirstName,
            LastName = person.LastName
        };
    }

    public static Person ToPerson(this PersonUpdate person)
    {
        return new Person
        {
            Id = person.Id,
            FirstName = person.FirstName,
            LastName = person.LastName
        };
    }
}
