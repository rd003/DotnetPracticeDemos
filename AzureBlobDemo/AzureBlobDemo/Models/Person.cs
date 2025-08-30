using System.ComponentModel.DataAnnotations;

namespace AzureBlobDemo.Models;

public class Person
{
    public int PersonId { get; set; }

    [Required]
    [MaxLength(30)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(30)]
    public string LastName { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string ProfilePicture { get; set; } = null!;
}