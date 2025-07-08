using System.ComponentModel.DataAnnotations;

namespace MinimalApis25.Models;

public class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
