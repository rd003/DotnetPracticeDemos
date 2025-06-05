namespace JsonCrud.Person;

public record CreatePersonDto(string FirstName, string LastName);

public record UpdatePersonDto(Guid Id, string FirstName, string LastName);
public record PersonDto(Guid Id, string FirstName, string LastName);
