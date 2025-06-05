using System.Text.Json.Serialization;

namespace JsonCrud.Person;

public class Person
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }


    [JsonConstructor]
    private Person(Guid id, string firstName, string lastName)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
    }

    public static Person Create(string firstName, string lastName)
    {
        ValidateInputs(firstName, lastName);
        return new Person(Guid.CreateVersion7(), firstName, lastName);
    }

    public void Update(string firstName, string lastName)
    {
        ValidateInputs(firstName, lastName);
        // Id = id;
        FirstName = firstName;
        LastName = lastName;
    }

    private static void ValidateInputs(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new ArgumentException("FirstName can not be empty", nameof(firstName));
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new ArgumentException("LastName can not be empty", nameof(lastName));
        }
    }
}

// public class Person
// {
//     public Guid Id { get; set; }
//     public string FirstName { get; set; } = string.Empty;
//     public string LastName { get; set; } = string.Empty;
// }