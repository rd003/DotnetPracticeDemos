using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace DapperBulkInsertDemo;

[ApiController]
[Route("api/people")]
public class PeopleController : ControllerBase
{
  private readonly IConfiguration _config;
  private readonly string _constr;
  private readonly ILogger<PeopleController> _logger;

  public PeopleController(IConfiguration config, ILogger<PeopleController> logger)
  {
    _config = config;
    _constr = config.GetConnectionString("Default");
    _logger = logger;
  }

  [HttpPost]
  public async Task<IActionResult> GetPeople()
  {
    // Data to insert
    List<Person> people = [
      new Person{FirstName="John",LastName="Doe"},
      new Person{FirstName="Jane",LastName="Doe"},
      new Person{FirstName="Mohan",LastName="Singh"},
      new Person{FirstName="Nitin",LastName="Kumar"}
    ];

    DataTable personTable = new();
    personTable.Columns.Add("FirstName", typeof(string));
    personTable.Columns.Add("LastName", typeof(string));

    // Populate datatable with person data

    foreach (var person in people)
    {
      personTable.Rows.Add(person.FirstName, person.LastName);
    }

    using IDbConnection connection = new SqlConnection(_constr);

    var parameters = new DynamicParameters();
    parameters.Add("@PersonData", personTable, DbType.Object, ParameterDirection.Input);

    await connection.ExecuteAsync(
      "usp_AddPeople",
      parameters,
      commandType: CommandType.StoredProcedure
    );

    return Created();
  }
}
