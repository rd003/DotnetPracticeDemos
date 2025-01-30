namespace SelectManyDemo.Models;

public class Employee
{
    public int EmployeeId { get; set; }
    public string Name { get; set; } = string.Empty;

    public int DepartmentId { get; set; }

    public Department Department { get; set; } = null!;

    public ICollection<EmployeeSkill> EmployeeSkills { get; set; } = [];
}