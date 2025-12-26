using System.ComponentModel.DataAnnotations;

namespace MySqlEfCore.Api.DTOs;

public class PersonCreate
{
    [Required]
    [MaxLength(30)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(30)]
    public string LastName { get; set; } = null!;
}
