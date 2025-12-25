// Models/PersonUpdate.cs
using System.ComponentModel.DataAnnotations;

namespace DapperMysql.Api.Models;

public class PersonUpdate
{
    public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(30)]
    public string LastName { get; set; } = null!;
}
