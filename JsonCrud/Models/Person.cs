namespace JsonCrud.Models;

public class Person
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; } 
    public string LastName { get; private set; }

    private Person()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
    }

    private Person(Guid id, string firstName, string lastName)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
    }

    public void Create(string firstName, string lastName)
    {
        Id = Guid.CreateVersion7();
        FirstName = firstName;
        LastName = lastName;
    }

    public void Update(Guid id,string firstName, string lastName)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
    }
}
