using System.ComponentModel.DataAnnotations;

namespace AzureBlobDemo.Models;

public class PersonUpdateDto
{
    public int PersonId { get; set; }

    [Required]
    [MaxLength(30)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(30)]
    public string LastName { get; set; } = null!;

    [Required]
    public string? ProfilePicture { get; set; }

    public IFormFile? File { get; set; }

    public string? SuccessMessage { get; set; }
    public string? ErrorMessage { get; set; }
}