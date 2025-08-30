using System.ComponentModel.DataAnnotations;

namespace AzureBlobDemo.Models;

public class PersonDto
{
    [Required]
    [MaxLength(30)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(30)]
    public string LastName { get; set; } = null!;

    [Required]
    public IFormFile File { get; set; } = null!;

    public string? SuccessMessage { get; set; }
    public string? ErrorMessage { get; set; }
}