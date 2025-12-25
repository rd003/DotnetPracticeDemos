using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CartesianExplosionExample.Models;

public class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public ICollection<Address> Addresses { get; set; } = [];
    public ICollection<Email> Emails { get; set; } = [];
}

public class Email
{
    public int Id { get; set; }

    [EmailAddress]
    [MaxLength(150)]
    public string? PersonEmail { get; set; }
    public int PersonId { get; set; }

    [JsonIgnore]
    public Person Person { get; set; } = null!;
}

public class Address 
{
    public int Id { get; set; }

    [Required]
    [MaxLength(150)]
    public string PersonAddress { get; set; } = null!;
    
    public int PersonId { get; set; }

    [JsonIgnore]
    public Person Person { get; set; } = null!;
}


public record PersonDto(int Id,string FirstName, string LastName);
public record AddressDto(int Id,string PersonAddress,int PersonId);
public record EmailDto(int Id,string PersonEmail, int PersonId);