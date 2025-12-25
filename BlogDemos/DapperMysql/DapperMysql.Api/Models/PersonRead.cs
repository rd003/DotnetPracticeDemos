using System;

// Models/PersonRead
namespace DapperMysql.Api.Models;

public class PersonRead
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}
